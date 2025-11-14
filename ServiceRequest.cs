using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    public partial class ServiceRequest : Form
    {
        private BinarySearchTree requestTree;
        private MaxHeap priorityHeap;
        private WorkflowGraph workflowGraph;

        public ServiceRequest()
        {
            InitializeComponent(); 

            InitializeData();
            LoadRequestsToGrid();
            LoadPriorityRequests();

            
        }

        private void InitializeData()
        {
            requestTree = new BinarySearchTree();
            priorityHeap = new MaxHeap();
            workflowGraph = new WorkflowGraph();

            var r1 = new ServiceRequestData(101, "Broken AC unit in Public Library.", "In Progress", 5);
            var r2 = new ServiceRequestData(305, "Request for new park benches.", "Completed", 1);
            var r3 = new ServiceRequestData(202, "Leaking faucet in public bathroom.", "Awaiting Parts", 3);
            var r4 = new ServiceRequestData(510, "New traffic lights needed.", "Submitted", 2);
            var r5 = new ServiceRequestData(150, "Network connection slow in major cities.", "Pending Assignment", 4);

            requestTree.Insert(r1); priorityHeap.Insert(r1);
            requestTree.Insert(r2); priorityHeap.Insert(r2);
            requestTree.Insert(r3); priorityHeap.Insert(r3);
            requestTree.Insert(r4); priorityHeap.Insert(r4);
            requestTree.Insert(r5); priorityHeap.Insert(r5);
        }

        private void LoadRequestsToGrid()
        {
            try
            {
                List<ServiceRequestData> allRequests = requestTree.GetRequestsInOrder();
                requestsGridView.DataSource = allRequests;
                requestsGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading all requests: " + ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPriorityRequests()
        {
            try
            {
                int topN = 5;
                List<ServiceRequestData> urgentList = priorityHeap.GetTopRequests(topN);

                priorityRequestsList.Items.Clear();
                priorityRequestsList.Items.Add($"--- Top {urgentList.Count} Urgent Requests (Heap Priority) ---");

                foreach (var request in urgentList)
                {
                    priorityRequestsList.Items.Add(request.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading priority list: " + ex.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTrack_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtRequestId.Text.Trim(), out int idToTrack))
            {
                ServiceRequestData foundRequest = requestTree.Find(idToTrack);

                if (foundRequest != null)
                {
                    List<string> nextStatuses = workflowGraph.GetValidNextStatuses(foundRequest.Status);
                    string validTransitions = nextStatuses.Count > 0 ? string.Join(", ", nextStatuses) : "None (Final State)";

                    lblStatusOutput.Text =
                        $"Request ID: {idToTrack}\n" +
                        $"Status: {foundRequest.Status}\n" +
                        $"Submitted: {foundRequest.SubmittedDate.ToShortDateString()}\n" +
                        $"Description: {foundRequest.Description}\n" +
                        $"Next Valid Statuses: {validTransitions}";

                    lblStatusOutput.ForeColor = Color.DarkGreen;
                }
                else
                {
                    lblStatusOutput.Text = $"Error: Request ID {idToTrack} was not found in the system.";
                    lblStatusOutput.ForeColor = Color.Red;
                }
            }
            else
            {
                lblStatusOutput.Text = "Please enter a valid numeric Request ID.";
                lblStatusOutput.ForeColor = Color.Orange;
            }
        }
    }
}

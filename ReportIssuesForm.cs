using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    public partial class ReportIssuesForm : Form
    {
        private string attachedFilePath = string.Empty;

        private static List<ReportedIssue> reportedIssues = new List<ReportedIssue>();

        public ReportIssuesForm()
        {
            InitializeComponent();
        }

        private void btnAttachMedia_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Attach Media";
            openFileDialog.Filter = "Image and Document Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.pdf;*.docx;*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                attachedFilePath = openFileDialog.FileName;
                lblAttachment.Text = $"Attached: {attachedFilePath}";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLocation.Text) ||
                cmbCategory.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(rtbDescription.Text))
            {
                MessageBox.Show("Please fill in all required fields before submitting.", "Error");
                return;
            }

            progressReport.Value = 0;
            for (int i = 0; i <= 100; i += 20)
            {
                progressReport.Value = i;
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
            }

            ReportedIssue newIssue = new ReportedIssue
            {
                Location = txtLocation.Text,
                Category = cmbCategory.SelectedItem.ToString(),
                Description = rtbDescription.Text,
                AttachedFilePath = string.IsNullOrEmpty(attachedFilePath) ? "No file attached" : attachedFilePath,
                DateReported = DateTime.Now
            };

            reportedIssues.Add(newIssue);

            MessageBox.Show("Issue successfully reported!\n\n" +
                $"Location: {txtLocation.Text}\n" +
                $"Category: {cmbCategory.SelectedItem}\n" +
                $"Description: {rtbDescription.Text}\n" +
                (string.IsNullOrEmpty(attachedFilePath) ? "No file attached" : $"Attached File: {attachedFilePath}"),
                "Report Submitted");

            txtLocation.Clear();
            cmbCategory.SelectedIndex = -1;
            rtbDescription.Clear();
            lblAttachment.Text = "No file attached.";
            progressReport.Value = 0;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReportIssuesForm_Load(object sender, EventArgs e)
        {

        }
    }
}
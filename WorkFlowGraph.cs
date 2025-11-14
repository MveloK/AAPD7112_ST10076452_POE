// WorkflowGraph.cs

using System;
using System.Collections.Generic;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    
    public class WorkflowGraph
    {
        private Dictionary<string, List<string>> adjacencyList;

        public WorkflowGraph()
        {
            adjacencyList = new Dictionary<string, List<string>>();
            InitializeWorkflow();
        }

        
        private void InitializeWorkflow()
        {

            AddEdge("Submitted", "Pending Assignment");

            AddEdge("Pending Assignment", "In Progress");
            AddEdge("Pending Assignment", "Canceled");

            AddEdge("In Progress", "Awaiting Parts");
            AddEdge("In Progress", "Completed");
            AddEdge("In Progress", "Pending Review");

            AddEdge("Awaiting Parts", "In Progress");

            AddEdge("Pending Review", "Completed");
            AddEdge("Pending Review", "In Progress"); 

           
            EnsureNodeExists("Completed");
            EnsureNodeExists("Canceled");
        }

       
        public void AddEdge(string source, string destination)
        {
            EnsureNodeExists(source);
            if (!adjacencyList[source].Contains(destination))
            {
                adjacencyList[source].Add(destination);
            }
            EnsureNodeExists(destination);
        }

        private void EnsureNodeExists(string status)
        {
            if (!adjacencyList.ContainsKey(status))
            {
                adjacencyList.Add(status, new List<string>());
            }
        }

       
        
        public bool IsTransitionValid(string currentStatus, string targetStatus)
        {
            if (adjacencyList.ContainsKey(currentStatus))
            {
                return adjacencyList[currentStatus].Contains(targetStatus);
            }
            return false;
        }

        
        public List<string> GetValidNextStatuses(string currentStatus)
        {
            if (adjacencyList.ContainsKey(currentStatus))
            {
                return adjacencyList[currentStatus];
            }
            return new List<string>();
        }
    }
}
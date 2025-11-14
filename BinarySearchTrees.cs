using System;
using System.Collections.Generic;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    
    public class BSTNode
    {
        public ServiceRequestData Data { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(ServiceRequestData data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    
    public class BinarySearchTree
    {
        private BSTNode root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(ServiceRequestData request)
        {
            root = InsertRecursive(root, request);
        }

        private BSTNode InsertRecursive(BSTNode current, ServiceRequestData request)
        {
            if (current == null)
            {
                return new BSTNode(request);
            }

           
            if (request.RequestID < current.Data.RequestID)
            {
                current.Left = InsertRecursive(current.Left, request);
            }
            else if (request.RequestID > current.Data.RequestID)
            {
                current.Right = InsertRecursive(current.Right, request);
            }

            return current;
        }

        public ServiceRequestData Find(int requestID)
        {
            return FindRecursive(root, requestID);
        }

        private ServiceRequestData FindRecursive(BSTNode current, int requestID)
        {
            if (current == null)
            {
                return null; 
            }

            if (requestID == current.Data.RequestID)
            {
                return current.Data; 
            }
            else if (requestID < current.Data.RequestID)
            {
                return FindRecursive(current.Left, requestID);
            }
            else 
            {
                return FindRecursive(current.Right, requestID);
            }
        }

        public List<ServiceRequestData> GetRequestsInOrder()
        {
            var list = new List<ServiceRequestData>();
            InOrderTraversal(root, list);
            return list;
        }

        private void InOrderTraversal(BSTNode node, List<ServiceRequestData> list)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, list);
                list.Add(node.Data);
                InOrderTraversal(node.Right, list);
            }
        }
    }
}
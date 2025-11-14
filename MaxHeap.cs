
using System;
using System.Collections.Generic;

namespace AAPD7112_ST10076452_MveloKhumalo
{
    public class MaxHeap
    {
        private List<ServiceRequestData> heap;

        public int Count => heap.Count;

        public MaxHeap()
        {
            heap = new List<ServiceRequestData>();
        }

        
        public void Insert(ServiceRequestData request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

       
        public ServiceRequestData ExtractMax()
        {
            if (heap.Count == 0)
            {
                return null;
            }

            ServiceRequestData max = heap[0];

            int lastIndex = heap.Count - 1;
            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex); 

            if (heap.Count > 0)
            {
                HeapifyDown(0); 
            }

            return max;
        }

       
        public ServiceRequestData PeekMax()
        {
            return heap.Count > 0 ? heap[0] : null;
        }

        
        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            while (index > 0 && heap[index].Severity > heap[parentIndex].Severity)
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        
        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int largest = index; 

            if (leftChild < heap.Count && heap[leftChild].Severity > heap[largest].Severity)
            {
                largest = leftChild;
            }

            
            if (rightChild < heap.Count && heap[rightChild].Severity > heap[largest].Severity)
            {
                largest = rightChild;
            }

            
            if (largest != index)
            {
                Swap(index, largest);
                HeapifyDown(largest);
            }
        }

        
        private void Swap(int i, int j)
        {
            ServiceRequestData temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

       
        public List<ServiceRequestData> GetTopRequests(int count)
        {
            List<ServiceRequestData> topRequests = new List<ServiceRequestData>();

            MaxHeap tempHeap = new MaxHeap();
            tempHeap.heap.AddRange(this.heap);

            for (int i = 0; i < count && tempHeap.Count > 0; i++)
            {
                topRequests.Add(tempHeap.ExtractMax());
            }
            return topRequests;
        }
    }
}
using System;
using System.IO;
using System.Collections.Generic;
using Gentree.Library.Extensions;

namespace CurseCase
{
    public class Suspects
    {
        private Tuple<string, int>[] data;
        public Tuple<string, int>[] Data
        {
            get => data;
            set => data = value;
        }

        // TODO
        public Suspects(GenTree tree, int size)
        {
            data = new Tuple<string, int>[size];
            for (int i = 0; i < size; i++)
            {
                data[i] = new Tuple<string, int>(tree.name, tree.suspicion);
            }

        }

        // TODO

        public void sort(int[] arr)
    {
        int n = arr.Length;
 
        // Build heap (rearrange array)
        for (int i = n / 2 - 1; i >= 0; i--)
            heapify(arr, n, i);
 
        // One by one extract an element from heap
        for (int i = n - 1; i > 0; i--) {
            // Move current root to end
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
 
            // call max heapify on the reduced heap
            heapify(arr, i, 0);
        }
    }
 
    // To heapify a subtree rooted with node i which is
    // an index in arr[]. n is size of heap
    void heapify(int[] arr, int n, int i)
    {
        int largest = i; // Initialize largest as root
        int l = 2 * i + 1; // left = 2*i + 1
        int r = 2 * i + 2; // right = 2*i + 2
 
        // If left child is larger than root
        if (l < n && arr[l] > arr[largest])
            largest = l;
 
        // If right child is larger than largest so far
        if (r < n && arr[r] > arr[largest])
            largest = r;
 
        // If largest is not root
        if (largest != i) {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;
 
            // Recursively heapify the affected sub-tree
            heapify(arr, n, largest);
        }
    }
 
    /* A utility function to print array of size n */
    static void printArray(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n; ++i)
            Console.Write(arr[i] + " ");
        Console.Read();
    }
        public void HeapSort()
        {
            int[] arr = new int[data.Length];
            int array = Data.Length;

            HeapSort ob = new HeapSort();
            ob.sort(arr);

        }

        // Utilitary method to print the array.
        public override string ToString()
        {
            return string.Join<Tuple<string, int>>(" | ", this.data);
        }
    }
}

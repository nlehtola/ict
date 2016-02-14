// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// PriorityQueue.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Interfaces;
using ICT.Core.DBC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ICT.Collections
{
	/// <summary>
	/// Priority queue class. The elements in this collection are considered 
	/// ("forced") to be sorted. In this type of collection, the client doesn't have 
	/// the control where the new elements added to the collection are going to 
	/// be located. Also, the client can only access the minimum value.
	/// </summary>
	public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public PriorityQueue()
		{
			// Initialize instance variables
			Elements = new List<T>();
		}

		/// <summary>
		/// Container 
		/// </summary>
		private List<T> Elements { get; set; }

		/// <summary>
		/// Add an element to the priority queue. The newly added element is 
		/// added to a specific position based on its value.
		/// </summary>
		/// <param name="element">Element to be added</param>
		public void Enqueue(T element)
		{
			// Add the element
			Elements.Add(element);

			// Find the right location for the newly added element
			PercolateUp(Count() - 1);
		}

		/// <summary>
		/// Remove the minimum value in the collection (the "smallest" element in
		/// the collection). 
		/// </summary>
		/// <returns>Minimum value in the collection</returns>
		public T Dequeue()
		{
			// Precondition
			Contract.Requires(!IsEmpty(), "The collection can't be empty");

			// Remove the minimum element
			var element = Peek();

			// Move the last element in the collection to the first position
			Elements[0] = Elements[Count() - 1];

			// Resize the container
			Elements.RemoveAt(Count() - 1);

			// Rearrange the collection
			if (Count() > 1)
			{
				PushDownRoot(0);
			}

			return element;
		}

		/// <summary>
		/// Access the minimum value in the collection (the "smallest" element in
		/// the collection). This method does NOT remove the element from the
		/// collection.
		/// </summary>
		/// <returns>Minimum value in the collection</returns>
		public T Peek()
		{
			// Precondition
			Contract.Requires(!IsEmpty(), "The collection can't be empty");

			// Remove the element
			var element = Elements[0];

			return element;
		}

		/// <summary>
		/// Removes all the elements from the collection.
		/// </summary>
		public void Clear()
		{
			// Remove all the elements
			Elements.Clear();
		}

		/// <summary>
		/// Return the number of elements in the collection.
		/// </summary>
		/// <returns>Number of elements in the collection</returns>
		public int Count()
		{
			// Return the number of elements
			return Elements.Count;
		}

		/// <summary>
		/// Return whether the collection is empty or not.
		/// </summary>
		/// <returns>True if the collection is empty, otherwise false</returns>
		public bool IsEmpty()
		{
			// Is it empty?
			return Count() == 0;
		}

		/// <summary>
		/// Generate/return the string-based information about the collection.
		/// </summary>
		/// <returns>String-based information about the collection</returns>
		public override string ToString()
		{
            var delimiter = ",";
			var info = string.Join(delimiter, Elements.Select(e => e.ToString()).ToArray());

            return info;
		}

		#region Private Members

		/// <summary>
		/// Get left child index
		/// </summary>
		/// <param name="elementIndex">Index of the target element</param>
		/// <returns>Left child index</returns>
		private int GetLeftChildIndex(int elementIndex)
		{
			// Get left child index
			return 2 * elementIndex + 1;
		}

		/// <summary>
		/// Get right child index
		/// </summary>
		/// <param name="elementIndex">Index of the target element</param>
		/// <returns>Left child index</returns>
		private int GetRightChildIndex(int elementIndex)
		{
			// Get right child index
			return GetLeftChildIndex(elementIndex) + 1;
		}

		/// <summary>
		/// Get parent index
		/// </summary>
		/// <param name="elementIndex">Index of the target element</param>
		/// <returns>Parent index</returns>
		private int GetParentIndex(int elementIndex)
		{
			// Get parent index
			return (elementIndex - 1) / 2;
		}

		/// <summary>
		/// Percolates up a leaf value. It takes a value at leaf in near-heap, 
		/// and pushes up to correct location.
		/// </summary>
		/// <param name="elementIndex">Index of the target element</param>
		private void PercolateUp(int elementIndex)
		{
			// Precondition
			Contract.Requires(elementIndex >= 0 && elementIndex < Count(), "Valid index");

			// Percolate up
			var parentIndex = GetParentIndex(elementIndex);
			var element = Elements[elementIndex];

			while (elementIndex > 0 && element.CompareTo(Elements[parentIndex]) < 0)
			{
				Elements[elementIndex] = Elements[parentIndex];

				elementIndex = parentIndex;
				parentIndex = GetParentIndex(elementIndex);
			}

			Elements[elementIndex] = element;
		}

		/// <summary>
		/// Pushes root down into near-heap constructing heap.
		/// </summary>
		/// <param name="rootIndex">Index of the root element</param>
		private void PushDownRoot(int rootIndex)
		{
			// Precondition
			Contract.Requires(rootIndex >= 0 && rootIndex < Count(), "Valid index");

			// Push down
			var heapSize = Count();
			var element = Elements[rootIndex];

			while (rootIndex < heapSize)
			{
				var childIndex = GetLeftChildIndex(rootIndex);

				if (childIndex < heapSize) 
				{
					if (GetRightChildIndex(rootIndex) < heapSize &&
						Elements[childIndex + 1].CompareTo(Elements[childIndex]) < 0)
					{
						childIndex++;
					}
					
					// Assert: childIndex indexes smaller of two children
					if (Elements[childIndex].CompareTo(element) < 0) 
					{
						Elements[rootIndex] = Elements[childIndex];
						rootIndex = childIndex;     // keep moving down
					} 
					else 
					{ 
						// Found right location
						Elements[rootIndex] = element;

						return;
					}
				} 
				else 
				{ 
					// At a leaf! insert and halt
					Elements[rootIndex] = element;

					return;
				}       
			}
		}

		#endregion
	}
}

// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// IPriorityQueue.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

namespace ICT.Collections.Interfaces
{
    /// <summary>
    /// Interface for the priority queue collection.
    /// </summary>
    public interface IPriorityQueue<T>
    {
        /// <summary>
        /// Add an element to the priority queue. The newly added element is 
        /// added to a specific position based on its value.
        /// </summary>
        /// <param name="element">Element to be added</param>
        void Enqueue(T element);

        /// <summary>
        /// Remove the minimum value in the collection (the "smallest" element in
        /// the collection). 
        /// </summary>
        /// <returns>Minimum value in the collection</returns>
        T Dequeue();

        /// <summary>
        /// Access the minimum value in the collection (the "smallest" element in
        /// the collection). This method does NOT remove the element from the
        /// collection.
        /// </summary>
        /// <returns>Minimum value in the collection</returns>
        T Peek();

        /// <summary>
        /// Removes all the elements from the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Return the number of elements in the collection.
        /// </summary>
        /// <returns>Number of elements in the collection</returns>
        int Count();

        /// <summary>
        /// Return whether the collection is empty or not.
        /// </summary>
        /// <returns>True if the collection is empty, otherwise false</returns>
        bool IsEmpty();
    }
}

// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// IVertex.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ICT.Collections.Interfaces
{
    /// <summary>
    /// Interface for the vertex entity (for the graph data structure).
    /// </summary>
    public interface IVertex<T> : IComparable
        where T : IComparable<T>
    {
        /// <summary>
        /// Label associated with the vertex.
        /// </summary>
        T Label { get; }

        /// <summary>
        /// Flag to indicate whether the vertex has been visited or not.
        /// </summary>
        bool Visited { get; set; }

        /// <summary>
        /// Weight (used in algorithmic solutions)
        /// </summary>
        int Weight { get; set; }

        /// <summary>
        /// Get the edges incidents from this vertex
        /// </summary>
        /// <returns>Edge collection</returns>
        IEnumerable<IEdge<T>> GetEdges();

        /// <summary>
        /// Remove all the incident edges from this vertex.
        /// </summary>
        void Clear();

        /// <summary>
        /// Return the number of incident edges from this vertex.
        /// </summary>
        /// <returns>Number of incident edges from this vertex</returns>
        int EdgeCount();

        /// <summary>
        /// Add the given edge to the edge collection.
        /// </summary>
        /// <param name="edge">Given edge</param>
        void AddEdge(IEdge<T> edge);

        /// <summary>
        /// Add the given edges to the edge collection.
        /// </summary>
        /// <param name="edge">Given edges</param>
        void AddEdges(IEnumerable<IEdge<T>> edges);

        /// <summary>
        /// Get the incident edge that has the given final vertex.
        /// </summary>
        /// <param name="finalVertex">Given final vertex</param>
        /// <returns>Incident edge that has the given final vertex</returns>
        IEdge<T> GetEdge(IVertex<T> finalVertex);

        /// <summary>
        /// Check whether the edge is in the collection or not
        /// </summary>
        /// <param name="edge">Given edge</param>
        /// <returns>True if the edge is in the collection, false otherwise</returns>
        bool ContainsEdge(IEdge<T> edge);

        /// <summary>
        /// Remove the given edge from the edge collection.
        /// </summary>
        /// <param name="edge">Given edge</param>
        void RemoveEdge(IEdge<T> edge);

        /// <summary>
        /// Check whether it is linked to the given vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>True if the vertex is linked, false otherwise</returns>
        bool IsLinkedTo(IVertex<T> vertex);
    }
}

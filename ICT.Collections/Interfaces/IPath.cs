// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// IPath.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ICT.Collections.Interfaces
{
    /// <summary>
    /// Interface for the path entity. A path entity represents a collection of
    /// edges in a graph.
    /// </summary>
    public interface IPath<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Get vertex collection.
        /// </summary>
        /// <returns>Vertex collection</returns>
        IEnumerable<IVertex<T>> GetVertices();

        /// <summary>
        /// Get the edge collection.
        /// </summary>
        /// <returns>Edge collection</returns>
        IEnumerable<IEdge<T>> GetEdges();

        /// <summary>
        /// Set the path using a collection of edges.
        /// </summary>
        /// <param name="edges">Given edge collection</param>
        void SetPath(IEnumerable<IEdge<T>> edges);

        /// <summary>
        /// Set the path using a collection of vertices.
        /// </summary>
        /// <param name="vertices">Given vertex collection</param>
        void SetPath(IEnumerable<IVertex<T>> vertices);

        /// <summary>
        /// Remove all the edges from the collection.
        /// </summary>
        void Clear();

        /// <summary>
        /// Get weight of the entire path.
        /// </summary>
        /// <returns>weight of the entire path</returns>
        int GetWeight();
    }
}

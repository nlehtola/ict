// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// IGraph.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ICT.Collections.Interfaces
{
    /// <summary>
    /// Interface for the graph data structure.
    /// </summary>
    public interface IGraph<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Get the vertex collection.
        /// </summary>
        /// <returns>Vertex collection</returns>
        IEnumerable<IVertex<T>> GetVertices();

        /// <summary>
        /// Get the vertex collection associated to the given labels.
        /// </summary>
        /// <param name="labels">Given labels</param>
        /// <returns>Vertex collection</returns>
        IEnumerable<IVertex<T>> GetVertices(IEnumerable<T> labels);

        /// <summary>
        /// Get the vertex associated to the given label.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Vertex associated to the given label</returns>
        IVertex<T> GetVertex(T label);

        /// <summary>
        /// Get the edge collection.
        /// </summary>
        /// <returns>Edge collection</returns>
        IEnumerable<IEdge<T>> GetEdges();

        /// <summary>
        /// Get the edge associated to the given label.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Edge associated to the given label</returns>
        IEdge<T> GetEdge(T label);

        /// <summary>
        /// Get the edge associated to the given labels.
        /// </summary>
        /// <param name="startLabel">Label of the start vertex</param>
        /// <param name="finalLabel">Label of the final vertex</param>
        /// <returns>Edge associated to the given labels</returns>
        IEdge<T> GetEdge(T startLabel, T finalLabel);

        /// <summary>
        /// Add a vertex to the graph. The vertex is identified by its label.
        /// </summary>
        /// <param name="label">Label of the vertex</param>
        void AddVertex(T label);

        /// <summary>
        /// Add an edge to the graph. This method assumes that the endpoint 
        /// vertices are already available and are identified by their
        /// labels.
        /// </summary>
        /// <param name="label">Label of the edge</param>
        /// <param name="startLabel">Label of the start vertex</param>
        /// <param name="finalLabel">Label of the final vertex</param>
        /// <param name="weight">Weight of the edge</param>
        void AddEdge(T label, T startLabel, T finalLabel, int weight);

        /// <summary>
        /// Remove all the elements (vertices and edges) from the graph.
        /// </summary>
        void Clear();

        /// <summary>
        /// Return the number of vertices in the graph.
        /// </summary>
        /// <returns>Number of vertices in the graph</returns>
        int VertexCount();

        /// <summary>
        /// Return the number of edges in the graph.
        /// </summary>
        /// <returns>Number of edges in the graph</returns>
        int EdgeCount();

        /// <summary>
        /// Return whether the graph is empty or not.
        /// </summary>
        /// <returns>True if the graph is empty, otherwise false</returns>
        bool IsEmpty();

        /// <summary>
        /// Reset the visited flag of the vertices.
        /// </summary>
        void Reset();
    }
}


// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// Vertex.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Interfaces;
using ICT.Core.DBC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICT.Collections
{
    /// <summary>
    /// Implementation of a path entity for use in graphs. 
    /// </summary>
    internal class Path<T> : IPath<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Path()
        {
            // Initialize instance variables
            Vertices = new List<IVertex<T>>();
            Edges = new List<IEdge<T>>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="edges">Collection of edges</param>
        public Path(IEnumerable<IEdge<T>> edges)
        {
            // Initialize instance variables
            Vertices = new List<IVertex<T>>();
            Edges = new List<IEdge<T>>();

            // Setup path
            SetPath(edges);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertices">Collection of vertices</param>
        public Path(IEnumerable<IVertex<T>> vertices)
        {
            // Initialize instance variables
            Vertices = new List<IVertex<T>>();
            Edges = new List<IEdge<T>>();

            // Setup path
            SetPath(vertices);
        }

        /// <summary>
        /// Edge collection.
        /// </summary>
        private List<IEdge<T>> Edges { get; set; }

        /// <summary>
        /// Vertex collection.
        /// </summary>
        private List<IVertex<T>> Vertices { get; set; }

        /// <summary>
        /// Get the edge collection.
        /// </summary>
        /// <returns>Edge collection</returns>
        public IEnumerable<IEdge<T>> GetEdges()
        {
            // Return the collection
            var edges = new List<IEdge<T>>(Edges);

            return edges;
        }

        /// <summary>
        /// Get vertex collection.
        /// </summary>
        /// <returns>Vertex collection</returns>
        public IEnumerable<IVertex<T>> GetVertices()
        {
            // Return the collection
            var vertices = new List<IVertex<T>>(Vertices);

            return vertices;
        }

        /// <summary>
        /// Set the path using a collection of edges.
        /// </summary>
        /// <param name="edges">Given edge collection</param>
        public void SetPath(IEnumerable<IEdge<T>> edges)
        {
            // Precondition
            Contract.Requires(edges.Count() > 0, "Valid number of edges");

            // Clear containers
            Clear();

            // Set the edges
            Edges.AddRange(edges);

            // Set the path based on the edges
            SetupFromEdges();

            // Post-condition
            Contract.Ensures(Edges.Count() > 0, "Valid number of edges");
            Contract.Ensures(Vertices.Count() > 0, "Valid number of vertices");
        }

        /// <summary>
        /// Set the path using a collection of vertices.
        /// </summary>
        /// <param name="vertices">Given vertex collection</param>
        public void SetPath(IEnumerable<IVertex<T>> vertices)
        {
            // Precondition
            Contract.Requires(vertices.Count() > 1, "Valid number of vertices");

            // Clear containers
            Clear();

            // Set the vertices
            Vertices.AddRange(vertices);

            // Set the path based on the vertices
            SetupFromVertices();

            // Post-condition
            Contract.Ensures(Edges.Count() > 0, "Valid number of edges");
            Contract.Ensures(Vertices.Count() > 0, "Valid number of vertices");
        }

        /// <summary>
        /// Remove all the edges from the collection.
        /// </summary>
        public void Clear()
        {
            // Remove all entities
            Vertices.Clear();
            Edges.Clear();
        }

        /// <summary>
        /// Get weight of the entire path.
        /// </summary>
        /// <returns>weight of the entire path</returns>
        public int GetWeight()
        {
            var weight = 0;

            // Traverse the edge collection
            Edges.ForEach(e => weight += e.Weight);

            return weight;
        }

        #region PrivateMembers

        /// <summary>
        /// Setup the path based on the vertices.
        /// </summary>
        private void SetupFromVertices()
        {
            // Traverse all the vertices...
            for (var index = 0; index < Vertices.Count - 1; index++)
            {
                var startVertex = Vertices[index];
                var finalVertex = Vertices[index + 1];
                var edge = startVertex.GetEdge(finalVertex);

                Edges.Add(edge);
            }
        }

        /// <summary>
        /// Setup the path based on the edges.
        /// </summary>
        private void SetupFromEdges()
        {
            // Traverse all the edges for the start vertices
            Edges.ForEach(e => Vertices.Add(e.StartVertex));

            // Get the final vertex
            Vertices.Add(Edges[Edges.Count - 1].FinalVertex);
        }

        /// <summary>
        /// Generate/return the string-based information about the path.
        /// </summary>
        /// <returns>String-based information about the path</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("{" + Environment.NewLine);

            Vertices.ForEach(v => sb.Append("    " + v.ToString() + Environment.NewLine));

            sb.Append("}" + Environment.NewLine);

            sb.Append("{" + Environment.NewLine);

            Edges.ForEach(e => sb.Append("    " + e.ToString() + Environment.NewLine));

            sb.Append("}");

            return sb.ToString();
        }

        #endregion
    }
}

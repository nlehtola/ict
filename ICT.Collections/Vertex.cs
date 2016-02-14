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
    /// Implementation of a vertex entity for use in graphs.  A vertex is capable 
    /// of holding a label and has a flag that can be set  to mark it as visited.
    /// </summary>
    internal class Vertex<T> : IVertex<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="label">Label associated to the vertex</param>
        public Vertex(T label)
        {
            // Precondition
            Contract.Requires(!Equals(label, null), "Valid label");

            // Initialize instance variables
            Edges = new List<IEdge<T>>();
            Label = label;
            Visited = false;
            Weight = 0;
        }

        /// <summary>
        /// Edges incidents from this vertex
        /// </summary>
        private List<IEdge<T>> Edges { get; set; }

        /// <summary>
        /// Label associated with the vertex.
        /// </summary>
        public T Label { get; private set; }

        /// <summary>
        /// Flag to indicate whether the vertex has been visited or not.
        /// </summary>
        public bool Visited { get; set; }

        /// <summary>
        /// Weight (used in algorithmic solutions)
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Get the edges incidents from this vertex
        /// </summary>
        /// <returns>Edge collection</returns>
        public IEnumerable<IEdge<T>> GetEdges()
        {
            // Return the incident edges from this vertex
            var edges = new List<IEdge<T>>(Edges);

            return edges;
        }

        /// <summary>
        /// Remove all the incident edges from this vertex.
        /// </summary>
        public void Clear()
        {
            // Remove all the incident edges from this vertex
            Edges.Clear();
        }

        /// <summary>
        /// Return the number of incident edges from this vertex.
        /// </summary>
        /// <returns>Number of incident edges from this vertex</returns>
        public int EdgeCount()
        {
            // Return the number of edges in the collection
            return Edges.Count;
        }

        /// <summary>
        /// Add the given edge to the edge collection.
        /// </summary>
        /// <param name="edge">Given edge</param>
        public void AddEdge(IEdge<T> edge)
        {
            // Precondition
            Contract.Requires(!Edges.Contains(edge), "Edge doesn't exist");

            // Add the edge to the collection
            Edges.Add(edge);
        }

        /// <summary>
        /// Add the given edges to the edge collection.
        /// </summary>
        /// <param name="edge">Given edges</param>
        public void AddEdges(IEnumerable<IEdge<T>> edges)
        {
            // Precondition
            foreach (var edge in edges)
            {
                Contract.Requires(!Edges.Contains(edge), "Edge doesn't exist");
            }
            
            // Add the edge to the collection
            foreach (var edge in edges)
            {
                Edges.Add(edge);
            }
        }

        /// <summary>
        /// Remove the given edge from the edge collection.
        /// </summary>
        /// <param name="edge">Given edge</param>
        public void RemoveEdge(IEdge<T> edge)
        {
            // Precondition
            Contract.Requires(Edges.Contains(edge), "Edge exists");

            // Remove the edge to the collection
            Edges.Remove(edge);
        }

        /// <summary>
        /// Check whether the edge is in the collection or not
        /// </summary>
        /// <param name="edge">Given edge</param>
        /// <returns>True if the edge is in the collection, false otherwise</returns>
        public bool ContainsEdge(IEdge<T> edge)
        {
            // Look for edge
            return Edges.Contains(edge);
        }

        /// <summary>
        /// Get the incident edge that has the given final vertex.
        /// </summary>
        /// <param name="finalVertex">Given final vertex</param>
        /// <returns>Incident edge that has the given final vertex</returns>
        public IEdge<T> GetEdge(IVertex<T> finalVertex)
        {
            try
            {
                // Look for edge
                var edge = Edges.FirstOrDefault(e => e.FinalVertex.Equals(finalVertex));

                return edge;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Check whether it is linked to the given vertex
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns>True if the vertex is linked, false otherwise</returns>
        public bool IsLinkedTo(IVertex<T> vertex)
        {
            try
            {
                // Look for the vertex in the edge collection
                var edge = Edges.FirstOrDefault(e => e.FinalVertex.Equals(vertex));

                return edge != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">Provided object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var typedObj = obj as Vertex<T>;

            // Could we cast it?
            if (typedObj == null)
            {
                return false;
            }

            // Compare the labels
            return Label.CompareTo(typedObj.Label) == 0;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            // Just use the parent class
            return base.GetHashCode();
        }

        /// <summary>
        /// Generate/return the string-based information about the vertex.
        /// </summary>
        /// <returns>String-based information about the vertex</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format("{{{0}", Label.ToString()));

            if (Edges.Count() > 0)
            {
                sb.Append(string.Format(",{{", Label.ToString()));

                var delimiter = ",";
                var info = string.Join(delimiter, Edges.Select(e => e.Label.ToString()).ToArray());

                sb.Append(info);

                sb.Append(string.Format("}}"));
            }

            sb.Append(string.Format("}}"));

            return sb.ToString();
        }

        /// <summary>
        /// Compare this instance with a given one
        /// </summary>
        /// <param name="obj">Given object</param>
        /// <returns>-1, 0 or 1 for <, == or > conditions</returns>
        public int CompareTo(object obj)
        {
            var other = obj as Vertex<T>;

            return Weight == other.Weight ? 0 :
                   Weight > other.Weight ? 1 : -1;
        }
    }
}

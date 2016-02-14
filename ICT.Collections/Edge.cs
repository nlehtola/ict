// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// Vertex.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Interfaces;
using ICT.Core.DBC;
using System;
using System.Text;

namespace ICT.Collections
{
    /// <summary>
    /// Implementation of an edge entity for use in graphs.  A vertex is capable 
    /// of holding a label, a weight and the end vertices.
    /// </summary>
    internal class Edge<T> : IEdge<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="label">Label associated to the edge</param>
        /// <param name="weight">Edge weight</param>
        /// <param name="startVertex">Start vertex</param>
        /// <param name="finalVertex">End vertex</param>
        public Edge(T label, int weight, IVertex<T> startVertex, IVertex<T> finalVertex)
        {
            // Precondition
            Contract.Requires(!Equals(label, null), "Valid label");
            Contract.Requires(weight > 0, "Valid weight");
            Contract.Requires(startVertex != null, "Valid vertex");
            Contract.Requires(finalVertex != null, "Valid vertex");

            // Initialize instance variables
            Label = label;
            Weight = weight;
            StartVertex = startVertex;
            FinalVertex = finalVertex;
        }

        /// <summary>
        /// Label associated with the edge.
        /// </summary>
        public T Label { get; private set; }

        /// <summary>
        /// Start vertex.
        /// </summary>
        public IVertex<T> StartVertex { get; private set; }

        /// <summary>
        /// End vertex.
        /// </summary>
        public IVertex<T> FinalVertex { get; private set; }

        /// <summary>
        /// Weight associated to the edge.
        /// </summary>
        public int Weight { get; private set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">Provided object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var typedObj = obj as Edge<T>;

            // Could we cast it?
            if (typedObj == null)
            {
                return false;
            }

            // Compare the labels
            return StartVertex.Equals(typedObj.StartVertex) && 
                   FinalVertex.Equals(typedObj.FinalVertex);
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
        /// Generate/return the string-based information about the edge.
        /// </summary>
        /// <returns>String-based information about the edge</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format("{{{0},{1},{{{2},{3}}}}}",
                Label.ToString(), Weight, StartVertex.Label.ToString(), FinalVertex.Label.ToString()));

            return sb.ToString();
        }
    }
}

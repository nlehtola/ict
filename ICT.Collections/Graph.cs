// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// Graph.cs
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
    /// Implementation of a graph collection. In this particular implementation,
    /// the graph entities are weighted and directed.
    /// </summary>
    public class Graph<T> : IGraph<T>
        where T : IComparable<T> 
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Graph()
        {
            // Initialize instance variables
            Vertices = new Dictionary<T, IVertex<T>>();
            Edges = new Dictionary<T, IEdge<T>>();
        }

        /// <summary>
        /// Vertex collection.
        /// </summary>
        private Dictionary<T, IVertex<T>> Vertices { get; set; }

        /// <summary>
        /// Edge collection.
        /// </summary>
        private Dictionary<T, IEdge<T>> Edges { get; set; }

        /// <summary>
        /// Get the vertex collection.
        /// </summary>
        /// <returns>Vertex collection</returns>
        public IEnumerable<IVertex<T>> GetVertices()
        {
            try
            {
                // Return the collection
                var vertices = Vertices.Values.ToList();

                if (vertices == null)
                {
                    return new List<IVertex<T>>();
                }

                return vertices;
            }
            catch
            {
                return new List<IVertex<T>>();
            }
        }

        /// <summary>
        /// Get the vertex collection associated to the given labels.
        /// </summary>
        /// <param name="labels">Given labels</param>
        /// <returns>Vertex collection</returns>
        public IEnumerable<IVertex<T>> GetVertices(IEnumerable<T> labels)
        {
            var vertices = new List<IVertex<T>>();

            try
            {
                // Return the collection
                foreach (var label in labels)
                {
                    var vertex = Vertices.Values.FirstOrDefault(v => label.CompareTo(v.Label) == 0);

                    if (vertex != null)
                    {
                        vertices.Add(vertex);
                    }
                }

                return vertices;
            }
            catch
            {
                return new List<IVertex<T>>();
            }
        }

        /// <summary>
        /// Get the vertex associated to the given label.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Vertex associated to the given label</returns>
        public IVertex<T> GetVertex(T label)
        {
            try
            {
                // Find the vertex
                var vertex = Vertices.Values.FirstOrDefault(v => v.Label.CompareTo(label) == 0);

                return vertex;
            }
            catch
            {
                // Problems...
                return null;
            }
        }

        /// <summary>
        /// Get the edge collection.
        /// </summary>
        /// <returns>Edge collection</returns>
        public IEnumerable<IEdge<T>> GetEdges()
        {
            // Return the collection
            try
            {
                var edges = Edges.Values.ToList();

                if (edges == null)
                {
                    return new List<IEdge<T>>();
                }

                return edges;
            }
            catch
            {
                return new List<IEdge<T>>();
            }
        }

        /// <summary>
        /// Get the edge associated to the given label.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Edge associated to the given label</returns>
        public IEdge<T> GetEdge(T label)
        {
            try
            {
                // Find the edge
                var edge = Edges.Values.FirstOrDefault(e => e.Label.CompareTo(label) == 0);

                return edge;
            }
            catch
            {
                // Problems...
                return null;
            }
        }

        /// <summary>
        /// Get the edge associated to the given labels.
        /// </summary>
        /// <param name="startLabel">Label of the start vertex</param>
        /// <param name="finalLabel">Label of the final vertex</param>
        /// <returns>Edge associated to the given labels</returns>
        public IEdge<T> GetEdge(T startLabel, T finalLabel)
        {
            try
            {
                // Find the edge
                var edge = Edges.Values.FirstOrDefault(e => 
                    e.StartVertex.Label.CompareTo(startLabel) == 0 && 
                    e.FinalVertex.Label.CompareTo(finalLabel) == 0);

                return edge;
            }
            catch
            {
                // Problems...
                return null;
            }
        }

        /// <summary>
        /// Add a vertex to the graph. The vertex is identified by its label.
        /// </summary>
        /// <param name="label">Label of the vertex</param>
        public void AddVertex(T label)
        {
            // Precondtion
            Contract.Requires(!Vertices.ContainsKey(label), "Vertex doesn't exist in the collection");

            // Create vertex
            var vertex = new Vertex<T>(label);

            // Add vertex to the collection
            Vertices.Add(label, vertex);
        }

        /// <summary>
        /// Add an edge to the graph. This method assumes that the endpoint 
        /// vertices are already available and are identified by their
        /// labels.
        /// </summary>
        /// <param name="label">Label of the edge</param>
        /// <param name="startLabel">Label of the start vertex</param>
        /// <param name="finalLabel">Label of the final vertex</param>
        /// <param name="weight">Weight of the edge</param>
        public void AddEdge(T label, T startLabel, T finalLabel, int weight)
        {
            // Precondtion
            Contract.Requires(!Equals(label, null), "Valid label");
            Contract.Requires(!Equals(weight, null), "Valid weight");
            Contract.Requires(Vertices.ContainsKey(startLabel), "Vertex exists in the collection");
            Contract.Requires(Vertices.ContainsKey(finalLabel), "Vertex exists in the collection");

            // Get vertices
            var startVertex = GetVertex(startLabel);
            var finalVertex = GetVertex(finalLabel);

            // Create edge
            var edge = new Edge<T>(label, weight, startVertex, finalVertex);

            // Add edge to the collection
            Edges.Add(label, edge);

            // Add edge reference to the start vertex
            startVertex.AddEdge(edge);
        }

        /// <summary>
        /// Remove all the elements (vertices and edges) from the graph.
        /// </summary>
        public void Clear()
        {
            // Remove all the elements from the graph
            Vertices.Clear();
            Edges.Clear();
        }

        /// <summary>
        /// Return the number of vertices in the graph.
        /// </summary>
        /// <returns>Number of vertices in the graph</returns>
        public int VertexCount()
        {
            // Return the number of vertices
            return Vertices.Count();
        }

        /// <summary>
        /// Return the number of edges in the graph.
        /// </summary>
        /// <returns>Number of edges in the graph</returns>
        public int EdgeCount()
        {
            // Return the number of edges
            return Edges.Count();
        }

        /// <summary>
        /// Return whether the graph is empty or not.
        /// </summary>
        /// <returns>True if the graph is empty, otherwise false</returns>
        public bool IsEmpty()
        {
            // Check whether is empty or not
            return VertexCount() == 0;
        }

        /// <summary>
        /// Reset the visited flag of the vertices.
        /// </summary>
        public void Reset()
        {
            // Reset the visited flag of the vertices
            var vertices = GetVertices();

            foreach (var vertex in vertices)
            {
                vertex.Visited = false;
            }
        }

        /// <summary>
        /// Generate/return the string-based information about the path.
        /// </summary>
        /// <returns>String-based information about the path</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("{" + Environment.NewLine);

            var vertices = Vertices.Values.ToList();
            vertices.ForEach(v => sb.Append("    " + v.ToString() + Environment.NewLine));

            sb.Append("}" + Environment.NewLine);

            sb.Append("{" + Environment.NewLine);

            var edges = Edges.Values.ToList();
            edges.ToList().ForEach(e => sb.Append("    " + e.ToString() + Environment.NewLine));

            sb.Append("}");

            return sb.ToString();
        }
    }
}

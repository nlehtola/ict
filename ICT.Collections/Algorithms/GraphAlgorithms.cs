// ---------------------------------------------------------------------------------
// ICT - ICT.Collections
// GraphAlgorithms.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Interfaces;
using ICT.Core.DBC;
using ICT.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ICT.Collections.Algorithms
{
    /// <summary>
    /// Algorithmic (extension) methods
    /// </summary>
    public static class GraphAlgorithms
    {
        /// <summary>
        /// Get the weight of a collection of vertices.
        /// </summary>
        /// <typeparam name="T">Base graph type</typeparam>
        /// <param name="graph">Graph</param>
        /// <param name="vertexLabels">Collection of vertex labels</param>
        /// <returns>The weight of the path that connects all the vertices</returns>
        public static int GetWeight<T>(this IGraph<T> graph, IEnumerable<T> vertexLabels) where T : IComparable<T>
        {
            try
            {
                // Get the path
                var path = graph.GetPath(vertexLabels);
                var weight = path.GetWeight();

                return weight;
            }
            catch (Exception ex)
            {
                // Pass the exception on...
                throw new CoreException(ex.Message);
            }
        }

        /// <summary>
        /// Get the minimum weight between two vertices.
        /// </summary>
        /// <typeparam name="T">Base graph type</typeparam>
        /// <param name="graph">Graph</param>
        /// <param name="startLabel">Label of the start vertex</param>
        /// <param name="finalLabel">Label of the final vertex</param>
        /// <returns></returns>
        public static int GetWeight<T>(this IGraph<T> graph, T startLabel, T finalLabel) where T : IComparable<T>
        {
            // Precondition
            var vertices = graph.GetVertices();
            var start = graph.GetVertex(startLabel);
            var final = graph.GetVertex(finalLabel);

            Contract.Requires(vertices != null && vertices.Count() > 0, "Graph is valid");
            Contract.Requires(start != null, "Start vertex is valid");
            Contract.Requires(final != null, "Final vertex is valid");

            // Set the weight of all vertices to a BIG number
            var list = new List<IVertex<T>>(vertices);

            list.ForEach(l => l.Weight = Int16.MaxValue);

            // Set start to 0...
            start.Weight = 0;

            // Process...
            while (list.Count > 0)
            {
                // Get the minimum value
                var minimum = list.Min();

                // Get edges incidents from the minimum vertex
                var edges = minimum.GetEdges();

                // Traverse the edge collection
                foreach (var edge in edges)
                {
                    var vertex = edge.FinalVertex;
                    var weight = edge.Weight + minimum.Weight;

                    if (vertex.Weight == 0 || vertex.Weight > weight)
                    {
                        vertex.Weight = weight;
                    }
                }

                // Remove the minimum vertex
                list.Remove(minimum);
            }

            vertices = graph.GetVertices();

            // Post-condition
            Contract.Ensures(final.Weight > 0 && final.Weight < Int16.MaxValue, "The path is valid");

            return final.Weight;
        }

        /// <summary>
        /// Get the collection of paths that start at a vertex. The criteria to stop the searching process is requested in 
        /// the process of invoking the "stopCriteria" delegate.
        /// </summary>
        /// <typeparam name="T">Base graph type</typeparam>
        /// <param name="graph">Graph</param>
        /// <param name="startLabel">Label of the start vertex</param>
        /// <param name="finalLabel">Label of the final vertex</param>
        /// <param name="maxNumEdges">Maximum number of stops</param>
        /// <returns></returns>
        public static IEnumerable<IPath<T>> GetPaths<T>(this IGraph<T> graph, T startLabel, T finalLabel,
                                                        int maxNumEdges, bool enforceExactNumEdges = false) where T : IComparable<T>
        {
            // Precondition
            Contract.Requires(maxNumEdges > 0, "Maximum number of edges is valid");

            // Get selected vertices
            var start = graph.GetVertex(startLabel);
            var final = graph.GetVertex(finalLabel);

            // Check the vertices
            Contract.Assert(start != null, "Start vertex is valid");
            Contract.Assert(final != null, "Final vertex is valid");

            // Reset vertices
            graph.Reset();

            // Create path container
            var paths = new List<IPath<T>>();

            // Create container of visited vertices
            var visited = new Stack<IVertex<T>>();

            // Go for the recursive call
            visited.Push(start);

            GetPathsRecursive<T>(paths, visited, start, final, maxNumEdges, 0, enforceExactNumEdges); 

            // Post-condtion
            Contract.Ensures(paths.Count > 0, "Valid set of paths");

            return paths;
        }

        /// <summary>
        /// Get the sequential path of a collection of vertices.
        /// </summary>
        /// <typeparam name="T">Base graph type</typeparam>
        /// <param name="graph">Graph</param>
        /// <param name="vertexLabels">Collection of vertex labels</param>
        /// <returns>The path that connects all the vertices</returns>
        public static IPath<T> GetPath<T>(this IGraph<T> graph, IEnumerable<T> vertexLabels) where T : IComparable<T>
        {
            // Precondition
            Contract.Requires(vertexLabels.Count() > 1, "Collection is valid (number of elements > 1)");

            // Get selected vertices
            var vertices = graph.GetVertices(vertexLabels);

            // Check the vertex collection
            Contract.Assert(vertices.Count() > 0, "Path is valid");

            // Check the sequence of links
            Contract.Assert(graph.IsLinked(vertices), "Vertices are connected as a path");

            // Create path
            var path = new Path<T>(vertices);

            return path;
        }

        /// <summary>
        /// Get the collection of paths that start at a vertex. The criteria to stop the searching process is requested in 
        /// the process of invoking the "stopCriteria" delegate.
        /// </summary>
        /// <typeparam name="T">Base graph type</typeparam>
        /// <param name="graph">Graph</param>
        /// <param name="startLabel">Label of the start vertex</param>
        /// <param name="finalLabel">Label of the final vertex</param>
        /// <param name="maxWeight">Maximum weight range</param>
        /// <returns></returns>
        public static IEnumerable<IPath<T>> GetPathsWithinRange<T>(this IGraph<T> graph, T startLabel, T finalLabel, int maxWeight) where T : IComparable<T>
        {
            // Precondition
            Contract.Requires(maxWeight > 0, "Maximum weight is valid");

            // Get selected vertices
            var start = graph.GetVertex(startLabel);
            var final = graph.GetVertex(finalLabel);

            // Check the vertices
            Contract.Assert(start != null, "Start vertex is valid");
            Contract.Assert(final != null, "Final vertex is valid");

            // Reset vertices
            graph.Reset();

            // Create path container
            var paths = new List<IPath<T>>();

            // Create container of visited vertices
            var visited = new Stack<IVertex<T>>();

            // Go for the recursive call
            visited.Push(start);

            GetPathsWithinRangeRecursive<T>(paths, visited, start, final, maxWeight, 0);

            // Post-condtion
            Contract.Ensures(paths.Count > 0, "Valid set of paths");

            return paths;
        }

        #region PrivateMembers

        /// <summary>
        /// Get paths recursively.
        /// </summary>
        /// <typeparam name="T">Base type</typeparam>
        /// <param name="paths">Path collection</param>
        /// <param name="visited">Container of visited elements</param>
        /// <param name="currVertex">Current vertex</param>
        /// <param name="finalVertex">Final vertex</param>
        /// <param name="maxWeight">Maximum weight</param>
        /// <param name="currWeight">Current weight</param>
        private static void GetPathsWithinRangeRecursive<T>(List<IPath<T>> paths, Stack<IVertex<T>> visited,
                                                            IVertex<T> currVertex, IVertex<T> finalVertex,
                                                            int maxWeight, int currWeight) where T : IComparable<T>
        {
            // Stop condition
            if (currWeight > 0 && currWeight >= maxWeight)
            {
                return;
            }

            if (currWeight > 0 && currVertex.Equals(finalVertex))
            {
                AddPath(paths, visited);
            }

            // Recursion
            var edges = currVertex.GetEdges();

            foreach (var edge in edges)
            {
                var vertex = edge.FinalVertex;

                visited.Push(vertex);

                GetPathsWithinRangeRecursive<T>(paths, visited, vertex, finalVertex, maxWeight, currWeight + edge.Weight);

                visited.Pop();
            }
        }

        /// <summary>
        /// Get paths recursively.
        /// </summary>
        /// <typeparam name="T">Base type</typeparam>
        /// <param name="paths">Path collection</param>
        /// <param name="visited">Container of visited elements</param>
        /// <param name="currVertex">Current vertex</param>
        /// <param name="finalVertex">Final vertex</param>
        /// <param name="maxNumEdges">Maximum number of edges</param>
        /// <param name="currLevel">Current edge level</param>
        /// <param name="enforceExactNumEdges">Enforce exact number of edges</param>
        private static void GetPathsRecursive<T>(List<IPath<T>> paths, Stack<IVertex<T>> visited,
                                                 IVertex<T> currVertex, IVertex<T> finalVertex,
                                                 int maxNumEdges, int currLevel, bool enforceExactNumEdges) where T : IComparable<T>
        {
            // Stop condition
            if (currLevel > 0)
            {
                if (!enforceExactNumEdges)
                {
                    if (visited.Count > maxNumEdges || currVertex.Equals(finalVertex))
                    {
                        if (currVertex.Equals(finalVertex))
                        {
                            AddPath(paths, visited);
                        }

                        return;
                    }
                }
                else
                {
                    if (visited.Count > maxNumEdges)
                    {
                        if (currVertex.Equals(finalVertex))
                        {
                            AddPath(paths, visited);
                        }

                        return;
                    }
                }
            }


            // Recursion
            currLevel++;

            var edges = currVertex.GetEdges();

            foreach (var edge in edges)
            {
                var vertex = edge.FinalVertex;

                visited.Push(vertex);

                GetPathsRecursive<T>(paths, visited, vertex, finalVertex, maxNumEdges, currLevel, enforceExactNumEdges);

                visited.Pop();
            }
        }

        /// <summary>
        /// Create/add a new path to the collection.
        /// </summary>
        /// <typeparam name="T">Basic type</typeparam>
        /// <param name="paths">Path collection</param>
        /// <param name="vertices">Vertex collection</param>
        private static void AddPath<T>(List<IPath<T>> paths, Stack<IVertex<T>> stack) where T : IComparable<T>
        {
            try
            {
                var vertices = stack.ToList();

                // Because it's a stack...
                vertices.Reverse();

                var path = new Path<T>(vertices);

                paths.Add(path);
            }
            catch
            {
                throw new CoreException("Invalid path");
            }
        }

        /// <summary>
        /// Create/add a new path to the collection.
        /// </summary>
        /// <typeparam name="T">Basic type</typeparam>
        /// <param name="paths">Path collection</param>
        /// <param name="targetVertex">Path collection</param>
        /// <param name="vertices">Vertex collection</param>
        private static void AddPath<T>(List<IPath<T>> paths, IVertex<T> targetVertex, Stack<IVertex<T>> stack) where T : IComparable<T>
        {
            try
            {
                var vertices = stack.ToList();

                // Because it's a stack...
                vertices.Reverse();

                // Find the target vertex from the end...
                var index = vertices.LastIndexOf(targetVertex);

                if (index == -1)
                {
                    return;
                }

                // Remove all from index on...
                var selection = vertices.GetRange(0, index + 1);

                var path = new Path<T>(selection);

                paths.Add(path);
            }
            catch
            {
                throw new CoreException("Invalid path");
            }
        }

        /// <summary>
        /// Check whether the collection of vertices is linked (as a path!)
        /// </summary>
        /// <typeparam name="T">Base graph type</typeparam>
        /// <param name="graph">Graph</param>
        /// <param name="vertices">Collection of vertices</param>
        /// <returns>True if the vertices are connected as a path, false otherwise</returns>
        private static bool IsLinked<T>(this IGraph<T> graph, IEnumerable<IVertex<T>> vertices) where T : IComparable<T>
        {
            try
            {
                // Check sequence
                var vertexArray = vertices.ToArray();

                for (var index = 0; index < vertices.Count() - 1; index++)
                {
                    var startVertex = vertexArray[index];
                    var finalVertex = vertexArray[index + 1];

                    if (!startVertex.IsLinkedTo(finalVertex))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}

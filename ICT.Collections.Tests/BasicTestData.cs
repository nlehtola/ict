// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// BasicTestData.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Sample data class (instance of this class will be used in the unit test for 
    /// the collections).
    /// </summary>
    internal class BasicTestData
    {
        /// <summary>
        /// Enum of path classes
        /// </summary>
        internal enum PathClass
        {
            ABCEdgeBased,
            ABCVertexBased,
            ADEdgeBased,
            ADVertexBased,
            ADCEdgeBased,
            ADCVertexBased,
            AEBCDEdgeBased,
            AEBCDVertexBased,
            AEDVertexBased
        };

        /// <summary>
        /// Constructor.
        /// </summary>
        internal BasicTestData()
        {
            // Initialize instance variables
            Graph = new Graph<string>();
            Vertices = new List<IVertex<string>>();
            Edges = new List<IEdge<string>>();
            Paths = new List<IPath<string>>();

            // Setup
            SetupBasicTestData();
        }

        /// <summary>
        /// Graph data structure
        /// </summary>
        private Graph<string> Graph { get; set; }

        /// <summary>
        /// Vertex collection.
        /// </summary>
        private List<IVertex<string>> Vertices { get; set; }

        /// <summary>
        /// Edge collection.
        /// </summary>
        private List<IEdge<string>> Edges { get; set; }

        /// <summary>
        /// Path collection.
        /// </summary>
        private List<IPath<string>> Paths { get; set; }

        /// <summary>
        /// Get vertex.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Target vertex</returns>
        internal IVertex<string> GetVertex(string label)
        {
            var vertex = Vertices.FirstOrDefault(v => v.Label.CompareTo(label) == 0);

            return vertex;
        }

        /// <summary>
        /// Get path.
        /// </summary>
        /// <param name="index">Given index</param>
        /// <returns>Target path</returns>
        internal IPath<string> GetPath(int index)
        {
            return Paths[index];
        }

        /// <summary>
        /// Get edge.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Target edge</returns>
        internal IEdge<string> GetEdge(string label)
        {
            var edge = Edges.FirstOrDefault(e => e.Label.CompareTo(label) == 0);

            return edge;
        }

        #region PrivateMembers

        /// <summary>
        /// Setup sample data.
        /// </summary>
        private void SetupBasicTestData()
        {
            // AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7
            // Vertices
            Vertices.Add(new Vertex<string>("A"));
            Vertices.Add(new Vertex<string>("B"));
            Vertices.Add(new Vertex<string>("C"));
            Vertices.Add(new Vertex<string>("D"));
            Vertices.Add(new Vertex<string>("E"));

            var vertexA = GetVertex("A");
            var vertexB = GetVertex("B");
            var vertexC = GetVertex("C");
            var vertexD = GetVertex("D");
            var vertexE = GetVertex("E");

            // Edges
            Edges.Add(new Edge<string>("AB", 5, vertexA, vertexB));
            Edges.Add(new Edge<string>("AD", 5, vertexA, vertexD));
            Edges.Add(new Edge<string>("AE", 7, vertexA, vertexE));

            Edges.Add(new Edge<string>("BC", 4, vertexB, vertexC));

            Edges.Add(new Edge<string>("CD", 8, vertexC, vertexD));
            Edges.Add(new Edge<string>("CE", 2, vertexC, vertexE));

            Edges.Add(new Edge<string>("DC", 8, vertexD, vertexC));
            Edges.Add(new Edge<string>("DE", 6, vertexD, vertexE));

            Edges.Add(new Edge<string>("EB", 3, vertexE, vertexB));

            var edgeAB = GetEdge("AB");
            var edgeAD = GetEdge("AD");
            var edgeAE = GetEdge("AE");

            var edgeBC = GetEdge("BC");

            var edgeCD = GetEdge("CD");
            var edgeCE = GetEdge("CE");

            var edgeDC = GetEdge("DC");
            var edgeDE = GetEdge("DE");

            var edgeEB = GetEdge("EB");

            vertexA.AddEdges(new IEdge<string>[] { edgeAB, edgeAD, edgeAE });
            vertexB.AddEdges(new IEdge<string>[] { edgeBC });
            vertexC.AddEdges(new IEdge<string>[] { edgeCD, edgeCE });
            vertexD.AddEdges(new IEdge<string>[] { edgeDC, edgeDE });
            vertexE.AddEdges(new IEdge<string>[] { edgeEB });

            // Paths
            // 0: A-B-C (edge-based)
            Paths.Add(new Path<string>(new IEdge<string>[] { edgeAB, edgeBC }));

            // 1: A-B-C (vertex-based)
            Paths.Add(new Path<string>(new IVertex<string>[] { vertexA, vertexB, vertexC }));

            // 2: A-D (edge-based)
            Paths.Add(new Path<string>(new IEdge<string>[] { edgeAD }));

            // 3: A-D (vertex-based)
            Paths.Add(new Path<string>(new IVertex<string>[] { vertexA, vertexD }));

            // 4: A-D-C (edge-based)
            Paths.Add(new Path<string>(new IEdge<string>[] { edgeAD, edgeDC }));

            // 5: A-D-C (vertex-based)
            Paths.Add(new Path<string>(new IVertex<string>[] { vertexA, vertexD, vertexC }));

            // 6: A-E-B-C-D (edge-based)
            Paths.Add(new Path<string>(new IEdge<string>[] { edgeAE, edgeEB, edgeBC, edgeCD }));

            // 7: A-E-B-C-D (vertex-based)
            Paths.Add(new Path<string>(new IVertex<string>[] { vertexA, vertexE, vertexB, vertexC, vertexD }));

            // 8: A-E-D (vertex-based)
            Paths.Add(new Path<string>(new IVertex<string>[] { vertexA, vertexE, vertexD }));
        }

        #endregion
    }
}

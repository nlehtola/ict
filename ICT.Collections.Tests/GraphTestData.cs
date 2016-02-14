// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// GraphTestData.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Interfaces;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Sample data class (instance of this class will be used in the unit test for 
    /// the collections).
    /// </summary>
    internal class GraphTestData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        internal GraphTestData()
        {
            // Initialize instance variables
            Graph = new Graph<string>();

            // Setup
            SetupGraphTestData();
        }

        /// <summary>
        /// Graph data structure
        /// </summary>
        private Graph<string> Graph { get; set; }

        /// <summary>
        /// Get graph.
        /// </summary>
        /// <returns>Graph</returns>
        internal IGraph<string> GetGraph()
        {
            return Graph;
        }

        /// <summary>
        /// Get vertex.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Target vertex</returns>
        internal IVertex<string> GetVertex(string label)
        {
            return Graph.GetVertex(label);
        }

        /// <summary>
        /// Get edge.
        /// </summary>
        /// <param name="label">Given label</param>
        /// <returns>Target edge</returns>
        internal IEdge<string> GetEdge(string label)
        {
            return Graph.GetEdge(label);
        }

        #region PrivateMembers

        /// <summary>
        /// Setup sample data.
        /// </summary>
        private void SetupGraphTestData()
        {
            // AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7
            // Vertices
            Graph.AddVertex("A");
            Graph.AddVertex("B");
            Graph.AddVertex("C");
            Graph.AddVertex("D");
            Graph.AddVertex("E");

            // Edges
            Graph.AddEdge("AB", "A", "B", 5);
            Graph.AddEdge("AD", "A", "D", 5);
            Graph.AddEdge("AE", "A", "E", 7);

            Graph.AddEdge("BC", "B", "C", 4);

            Graph.AddEdge("CD", "C", "D", 8);
            Graph.AddEdge("CE", "C", "E", 2);

            Graph.AddEdge("DC", "D", "C", 8);
            Graph.AddEdge("DE", "D", "E", 6);

            Graph.AddEdge("EB", "E", "B", 3);
        }

        #endregion
    }
}

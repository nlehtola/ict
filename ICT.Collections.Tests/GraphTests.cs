// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// GraphTests.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Summary description for GraphTests.
    /// </summary>
    [TestClass]
    public class GraphTests
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Sample data for the tests
        /// </summary>
        private GraphTestData GraphTestData { get; set; }

        #region Additional test attributes

        [TestInitialize]
        public void TestInitialize()
        {
            // Create sample data
            GraphTestData = new GraphTestData();
        }

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void Graph_Constructor_NullVertices_Fail()
        {
            // Arrange
            var edge = new Edge<int>(0, 0, null, null);

            // Act
            // ...

            // Assert
            // ...
        }

        [TestMethod]
        public void Graph_ToString_Execution_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected =
@"{
    {A,{AB,AD,AE}}
    {B,{BC}}
    {C,{CD,CE}}
    {D,{DC,DE}}
    {E,{EB}}
}
{
    {AB,5,{A,B}}
    {AD,5,{A,D}}
    {AE,7,{A,E}}
    {BC,4,{B,C}}
    {CD,8,{C,D}}
    {CE,2,{C,E}}
    {DC,8,{D,C}}
    {DE,6,{D,E}}
    {EB,3,{E,B}}
}";
            // Act
            var result = graph.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_IsEmpty_False_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();

            // Act
            var result = graph.IsEmpty();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Graph_VertexCount_Count5_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 5;

            // Act
            var result = graph.VertexCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_EdgeCount_Count9_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 9;

            // Act
            var result = graph.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_Clear_EdgeCount0_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 0;

            // Act
            graph.Clear();

            var result = graph.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_Clear_VertexCount0_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 0;

            // Act
            graph.Clear();

            var result = graph.VertexCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void Graph_AddVertex_VertexAlreadyExists_ThrowsException()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var vertexLabel = "A";

            // Act
            graph.AddVertex(vertexLabel);

            // Assert
            // ...
        }

        [TestMethod]
        public void Graph_GetVertices_AllVertices_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{A,{AB,AD,AE}} {B,{BC}} {C,{CD,CE}} {D,{DC,DE}} {E,{EB}}";

            // Act
            var vertices = graph.GetVertices();
            var result = "";

            foreach (var vertex in vertices)
            {
                result += vertex.ToString() + " ";
            }

            result = result.Trim();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetVertex_A_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{A,{AB,AD,AE}}";

            // Act
            var vertexLabel = "A";
            var vertex = graph.GetVertex(vertexLabel);
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetVertex_B_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{B,{BC}}";

            // Act
            var vertexLabel = "B";
            var vertex = graph.GetVertex(vertexLabel);
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetVertex_C_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{C,{CD,CE}}";

            // Act
            var vertexLabel = "C";
            var vertex = graph.GetVertex(vertexLabel);
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetVertex_D_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{D,{DC,DE}}";

            // Act
            var vertexLabel = "D";
            var vertex = graph.GetVertex(vertexLabel);
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetVertex_E_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{E,{EB}}";

            // Act
            var vertexLabel = "E";
            var vertex = graph.GetVertex(vertexLabel);
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetVertex_VertexDoesNotExist_Fail()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();

            // Act
            var vertexLabel = "X";
            var result = graph.GetVertex(vertexLabel);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Graph_GetVertices_SomeVertices_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{B,{BC}} {C,{CD,CE}} {E,{EB}}";

            // Act
            var labels = new string[] { "B", "C", "E" };
            var vertices = graph.GetVertices(labels);
            var result = "";

            foreach (var vertex in vertices)
            {
                result += vertex.ToString() + " ";
            }

            result = result.Trim();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetEdges_AllEdges_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{AB,5,{A,B}} {AD,5,{A,D}} {AE,7,{A,E}} {BC,4,{B,C}} {CD,8,{C,D}} {CE,2,{C,E}} {DC,8,{D,C}} {DE,6,{D,E}} {EB,3,{E,B}}";

            // Act
            var edges = graph.GetEdges();
            var result = "";

            foreach (var edge in edges)
            {
                result += edge.ToString() + " ";
            }

            result = result.Trim();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetEdge_AB_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{AB,5,{A,B}}";

            // Act
            var edgeLabel = "AB";
            var edge = graph.GetEdge(edgeLabel);
            var result = edge.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetEdge_AD_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{AD,5,{A,D}}";

            // Act
            var edgeLabel = "AD";
            var edge = graph.GetEdge(edgeLabel);
            var result = edge.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetEdge_DC_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{DC,8,{D,C}}";

            // Act
            var edgeLabel = "DC";
            var edge = graph.GetEdge(edgeLabel);
            var result = edge.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetEdge_CEWithVertices_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = "{CE,2,{C,E}}";

            // Act
            var startLabel = "C";
            var finalLabel = "E";
            var edge = graph.GetEdge(startLabel, finalLabel);
            var result = edge.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Graph_GetEdge_EdgeDoesNotExist_Fail()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();

            // Act
            var edgeLabel = "X";
            var result = graph.GetEdge(edgeLabel);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Graph_GetEdge_VerticesDoNotExist_Fail()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();

            // Act
            var startLabel = "X";
            var finalLabel = "Y";
            var result = graph.GetEdge(startLabel, finalLabel);

            // Assert
            Assert.IsNull(result);
        }

    }
}



    


        //IEnumerable<IEdge<T>> GetEdges();
        //IEdge<T> GetEdge(T label);
        //IEdge<T> GetEdge(T startLabel, T finalLabel);


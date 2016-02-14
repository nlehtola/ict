// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// VertexTests.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Summary description for VertexTests.
    /// </summary>
    [TestClass]
    public class VertexTests
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Sample data for the tests
        /// </summary>
        private BasicTestData BasicTestData { get; set; }

        #region Additional test attributes

        [TestInitialize]
        public void TestInitialize()
        {
            // Create sample data
            BasicTestData = new BasicTestData();
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
        public void Vertex_Constructor_LabelInt_Success()
        {
            // Arrange
            var vertex = new Vertex<int>(10);

            // Act
            // ...

            // Assert
            Assert.IsNotNull(vertex);
        }

        [TestMethod]
        public void Vertex_Constructor_LabelString_Success()
        {
            // Arrange
            var vertex = new Vertex<string>("label");

            // Act
            // ...

            // Assert
            Assert.IsNotNull(vertex);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void Vertex_Constructor_LabelStringNull_ThrowsException()
        {
            // Arrange
            var vertex = new Vertex<string>(null);

            // Act
            // ...

            // Assert
            // ...
        }

        [TestMethod]
        public void Vertex_GetLabel_Int_Success()
        {
            // Arrange
            var vertex = new Vertex<int>(10);
            var expected = 10;

            // Act
            var result = vertex.Label;

            // Assert
            Assert.IsTrue(result.CompareTo(expected) == 0);
        }

        [TestMethod]
        public void Vertex_GetLabel_String_Success()
        {
            // Arrange
            var vertex = new Vertex<string>("label");
            var expected = "label";

            // Act
            var result = vertex.Label;

            // Assert
            Assert.IsTrue(result.CompareTo(expected) == 0);
        }

        [TestMethod]
        public void Vertex_GetLabel_String_Fail()
        {
            // Arrange
            var vertex = new Vertex<string>("label");
            var expected = "Label";

            // Act
            var result = vertex.Label;

            // Assert
            Assert.IsFalse(result.CompareTo(expected) == 0);
        }

        [TestMethod]
        public void Vertex_Visited_SetToTrue_Success()
        {
            // Arrange
            var vertex = new Vertex<string>("label");
            var expected = true;

            // Act
            vertex.Visited = true;

            var result = vertex.Visited;

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Vertex_Reset_VisitedIsFalse_Success()
        {
            // Arrange
            var vertex = new Vertex<string>("label");
            var expected = false;

            // Act
            vertex.Visited = false;

            var result = vertex.Visited;

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Vertex_Equals_IntLabelVertices_Success()
        {
            // Arrange
            var vertex1 = new Vertex<int>(10);
            var vertex2 = new Vertex<int>(10);

            // Act
            // ...

            // Assert
            Assert.AreEqual(vertex1, vertex2);
        }

        [TestMethod]
        public void Vertex_Equals_IntLabelVertices_Fail()
        {
            // Arrange
            var vertex1 = new Vertex<int>(10);
            var vertex2 = new Vertex<int>(20);

            // Act
            // ...

            // Assert
            Assert.AreNotEqual(vertex1, vertex2);
        }

        [TestMethod]
        public void Vertex_Equals_StringLabelVertices_Success()
        {
            // Arrange
            var vertex1 = new Vertex<string>("Obla-Di");
            var vertex2 = new Vertex<string>("Obla-Di");

            // Act
            // ...

            // Assert
            Assert.AreEqual(vertex1, vertex2);
        }

        [TestMethod]
        public void Vertex_Equals_StringLabelVertices_Fail()
        {
            // Arrange
            var vertex1 = new Vertex<string>("Obla-Di");
            var vertex2 = new Vertex<string>("obla-di");

            // Act
            // ...

            // Assert
            Assert.AreNotEqual(vertex1, vertex2);
        }

        [TestMethod]
        public void Vertex_Equals_StringAndIntVertices_Fail()
        {
            // Arrange
            var vertex1 = new Vertex<string>("Obla-Di");
            var vertex2 = new Vertex<int>(10);

            // Act
            // ...

            // Assert
            Assert.AreNotEqual(vertex1, vertex2);
        }

        [TestMethod]
        public void Vertex_AddEdge_OneEdge_Success()
        {
            // Arrange
            var vertex1 = new Vertex<string>("Vertex 1");
            var vertex2 = new Vertex<string>("Vertex 2");
            var edge = new Edge<string>("Edge 1-2", 100, vertex1, vertex2);
            var expected = 1;

            // Act
            vertex1.AddEdge(edge);

            var result = vertex1.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Vertex_AddEdge_TwoEdges_Success()
        {
            // Arrange
            var vertex = BasicTestData.GetVertex("C");
            var expected = 2;

            // Act
            var result = vertex.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Vertex_AddEdge_TwoEdgesRemoveOne_Success()
        {
            // Arrange
            var vertex = BasicTestData.GetVertex("C");
            var edge = BasicTestData.GetEdge("CE");
            var expected = 1;

            // Act
            vertex.RemoveEdge(edge);

            var result = vertex.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Vertex_AddEdge_TwoEdgesRemoveAll_Success()
        {
            // Arrange
            var vertex = BasicTestData.GetVertex("C");
            var expected = 0;

            // Act
            vertex.Clear();

            var result = vertex.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void Vertex_AddEdge_SameEdge_ThrowsException()
        {
            // Arrange
            var vertexA = BasicTestData.GetVertex("A");
            var vertexB = BasicTestData.GetVertex("B");
            var edge = new Edge<string>("AB", 5, vertexA, vertexB);
            var expected = 3;

            // Act
            vertexA.AddEdge(edge);

            var result = vertexA.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void Vertex_RemoveEdge_EdgeDoNotExist_ThrowsException()
        {
            // Arrange
            var vertex = BasicTestData.GetVertex("A");
            var edge = BasicTestData.GetEdge("DC");
            var expected = 3;

            // Act
            vertex.RemoveEdge(edge);

            var result = vertex.EdgeCount();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Vertex_ToString_Execution_Success()
        {
            // Arrange
            var vertex = BasicTestData.GetVertex("A");
            var expected = "{A,{AB,AD,AE}}";

            // Act
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Vertex_ContainsEdge_Execution_Success()
        {
            // Arrange
            var vertex = BasicTestData.GetVertex("A");
            var edge = BasicTestData.GetEdge("AB");

            // Act
            var result = vertex.ContainsEdge(edge);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Vertex_ContainsEdge_Execution_Fail()
        {
            // Arrange
            var vertex = BasicTestData.GetVertex("A");
            var edge = BasicTestData.GetEdge("CD");

            // Act
            var result = vertex.ContainsEdge(edge);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Vertex_IsLinkedTo_Execution_Success()
        {
            // Arrange
            var vertexA = BasicTestData.GetVertex("A");
            var vertexB = BasicTestData.GetVertex("B");

            // Act
            var result = vertexA.IsLinkedTo(vertexB);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Vertex_IsLinkedTo_Execution_Fail()
        {
            // Arrange
            var vertexA = BasicTestData.GetVertex("A");
            var vertexC = BasicTestData.GetVertex("C");

            // Act
            var result = vertexA.IsLinkedTo(vertexC);

            // Assert
            Assert.IsFalse(result);
        }
    }
}

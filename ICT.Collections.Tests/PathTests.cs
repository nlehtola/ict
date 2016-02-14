// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// PathTests.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Interfaces;
using ICT.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PathClass = ICT.Collections.Tests.BasicTestData.PathClass;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Summary description for PathTests.
    /// </summary>
    [TestClass]
    public class PathTests
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
        public void Path_Constructor_Execution_Success()
        {
            // Arrange
            var path = new Path<string>();

            // Act
            // ...

            // Assert
            Assert.IsNotNull(path);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void Path_Constructor_EmptyEdgeCollection_ThrowsException()
        {
            // Arrange
            var edges = new List<IEdge<string>>();
            var path = new Path<string>(edges);

            // Act
            // ...

            // Assert
            Assert.IsNotNull(path);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void Path_Constructor_EmptyVertexCollection_ThrowsException()
        {
            // Arrange
            var vertices = new List<IVertex<string>>();
            var path = new Path<string>(vertices);

            // Act
            // ...

            // Assert
            Assert.IsNotNull(path);
        }

        [TestMethod]
        public void Path_ToString_ExecutionEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ADEdgeBased);
            var expected = 
@"{
    {A,{AB,AD,AE}}
    {D,{DC,DE}}
}
{
    {AD,5,{A,D}}
}";
            // Act
            var result = path.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_ToString_ExecutionVertexBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ADVertexBased);
            var expected =
@"{
    {A,{AB,AD,AE}}
    {D,{DC,DE}}
}
{
    {AD,5,{A,D}}
}";
            // Act
            var result = path.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_ToString_ExecutionVertexAndEdgeBased_Success()
        {
            // Arrange
            var pathEdgeBased = BasicTestData.GetPath((int)PathClass.ADEdgeBased);
            var pathVertexBased = BasicTestData.GetPath((int)PathClass.ADVertexBased);

            // Act
            var edgeBased = pathEdgeBased.ToString();
            var vertexBased = pathVertexBased.ToString();

            // Assert
            Assert.AreEqual(edgeBased, vertexBased);
        }

        [TestMethod]
        public void Path_GetVertices_ABCDEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ABCEdgeBased);
            var expected = "{A,{AB,AD,AE}} {B,{BC}} {C,{CD,CE}}";

            // Act
            var vertices = path.GetVertices();
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
        public void Path_GetEdges_ABCDEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ABCEdgeBased);
            var expected = "{AB,5,{A,B}} {BC,4,{B,C}}";

            // Act
            var edges = path.GetEdges();
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
        public void Path_Clear_ABCEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ABCEdgeBased);
            var expected = 0;

            // Act
            path.Clear();

            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_ABCEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ABCEdgeBased);
            var expected = 9;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_ABCDVertexBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ABCVertexBased);
            var expected = 9;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_ADEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ADEdgeBased);
            var expected = 5;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_ADVertexBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ADVertexBased);
            var expected = 5;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_ADCEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ADCEdgeBased);
            var expected = 13;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_ADCVertexBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.ADCVertexBased);
            var expected = 13;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_AEBCDEdgeBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.AEBCDEdgeBased);
            var expected = 22;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Path_GetWeight_AEBCDVertexBased_Success()
        {
            // Arrange
            var path = BasicTestData.GetPath((int)PathClass.AEBCDVertexBased);
            var expected = 22;

            // Act
            var result = path.GetWeight();

            // Assert
            Assert.AreEqual(result, expected);
        }

        //[TestMethod]
        //public void Path_GetWeight_AEDVertexBased_Success()
        //{
        //    // Arrange
        //    var path = BasicTestData.GetPath((int)PathClass.AEDVertexBased);
        //    var expected = 22;

        //    // Act
        //    var result = path.GetWeight();

        //    // Assert
        //    Assert.AreEqual(result, expected);
        //}
    }
}

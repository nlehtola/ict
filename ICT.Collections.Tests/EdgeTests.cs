// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// EdgeTests.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Summary description for EdgeTests.
    /// </summary>
    [TestClass]
    public class EdgeTests
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
        [ExpectedException(typeof(CoreException))]
        public void Edge_Constructor_NullVertices_Fail()
        {
            // Arrange
            var edge = new Edge<int>(0, 0, null, null);

            // Act
            // ...

            // Assert
            // ...
        }

        [TestMethod]
        public void Edge_StartVertex_Execution_Success()
        {
            // Arrange
            var edge = BasicTestData.GetEdge("AB");
            var vertex = edge.StartVertex;
            var expected = "{A,{AB,AD,AE}}";

            // Act
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Edge_FinalVertex_Execution_Success()
        {
            // Arrange
            var edge = BasicTestData.GetEdge("AB");
            var vertex = edge.FinalVertex;
            var expected = "{B,{BC}}";

            // Act
            var result = vertex.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Edge_Equals_SameEdges_Success()
        {
            // Arrange
            var edgeAB = BasicTestData.GetEdge("AB");
            var edgeABX = BasicTestData.GetEdge("AB");


            // Act
            var result = edgeAB.Equals(edgeABX);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Edge_Equals_DifferentEdges_Fail()
        {
            // Arrange
            var edgeAB = BasicTestData.GetEdge("AB");
            var edgeCD = BasicTestData.GetEdge("CD");


            // Act
            var result = edgeAB.Equals(edgeCD);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Edge_Weight_Equals5_Success()
        {
            // Arrange
            var edge = BasicTestData.GetEdge("AB");
            var expected = 5;

            // Act
            var result = edge.Weight;

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Edge_ToString_Execution_Success()
        {
            // Arrange
            var edge = BasicTestData.GetEdge("AB");
            var expected = "{AB,5,{A,B}}";

            // Act
            var result = edge.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}

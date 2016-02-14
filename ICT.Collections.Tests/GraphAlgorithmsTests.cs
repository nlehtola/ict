// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// GraphAlgorithmsTests.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Algorithms;
using ICT.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Summary description for GraphAlgorithmsTests.
    /// </summary>
    [TestClass]
    public class GraphAlgorithmsTests
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
        public void GraphAlgorithms_GetWeight_ABC_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 9;

            // Act
            var labels = new string[] { "A", "B", "C" };
            var result = graph.GetWeight(labels);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GraphAlgorithms_GetWeight_AD_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 5;

            // Act
            var labels = new string[] { "A", "D" };
            var result = graph.GetWeight(labels);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GraphAlgorithms_GetWeight_ADC_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 13;

            // Act
            var labels = new string[] { "A", "D", "C" };
            var result = graph.GetWeight(labels);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GraphAlgorithms_GetWeight_AEBCD_Success()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 22;

            // Act
            var labels = new string[] { "A", "E", "B", "C", "D" };
            var result = graph.GetWeight(labels);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void GraphAlgorithms_GetWeight_AED_ThrowsException()
        {
            // Arrange
            var graph = GraphTestData.GetGraph();

            // Act
            var labels = new string[] { "A", "E", "D" };
            var result = graph.GetWeight(labels);

            // Assert
            // ...
        }
    }
}

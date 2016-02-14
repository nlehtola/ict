// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// GraphAlgorithmsQuestionsTests.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Collections.Algorithms;
using ICT.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Summary description for GraphAlgorithmsQuestionsTests. 
    /// Note: The units tests in this module are related to the questions in the
    /// assessment (from 1 to 10)
    /// </summary>
    [TestClass]
    public class GraphAlgorithmsQuestionsTests
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




//7. 
//8. 
//9. 
//10. The number of different routes from C to C with a distance of less than 30. In the sample data, the trips are: CDC, CEBC, CEBCDC, CDCEBC, CDEBC, CEBCEBC, CEBCEBCEBC.

//Test Input:
//For the test input, the towns are named using the first few letters of the alphabet from A to D. A route between two towns (A to B) with a distance of 5 is represented as AB5.
//Graph: AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7
//Expected Output:




//Output #8: 9
//Output #9: 9
//Output #10: ?
//We
        [TestMethod]
        public void GraphAlgorithms_Question1_Success()
        {
            // Question 1: The distance of the route A-B-C.
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
        public void GraphAlgorithms_Question2_Success()
        {
            // Question 2: The distance of the route A-D.
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
        public void GraphAlgorithms_Question3_Success()
        {
            // Question 3: The distance of the route A-D-C.
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
        public void GraphAlgorithms_Question4_Success()
        {
            // Question 4: The distance of the route A-E-B-C-D.
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
        public void GraphAlgorithms_Question5_ThrowsException()
        {
            // Question 5: The distance of the route A-E-D.
            // Arrange
            var graph = GraphTestData.GetGraph();

            // Act
            var labels = new string[] { "A", "E", "D" };
            var result = graph.GetWeight(labels);

            // Assert
            // ..
        }

        [TestMethod]
        public void GraphAlgorithms_Question6_Success()
        {
            // Question 6: The number of trips starting at C and ending at C with a maximum of 3 stops.
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 2;

            // Act
            var startLabel = "C";
            var finalLabel = "C";
            var maxNumEdges = 3;
            var paths = graph.GetPaths(startLabel, finalLabel, maxNumEdges);
            var result = paths.Count();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GraphAlgorithms_Question7_Success()
        {
            // Question 7: The number of trips starting at A and ending at C with exactly 4 stops. 
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 3;

            // Act
            var startLabel = "A";
            var finalLabel = "C";
            var maxNumEdges = 4;
            var paths = graph.GetPaths(startLabel, finalLabel, maxNumEdges, true);
            var result = paths.Count();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GraphAlgorithms_Question8_Success()
        {
            // Question 8: The length of the shortest route (in terms of distance to travel) from A to C.
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 9;

            // Act
            var startLabel = "A";
            var finalLabel = "C";
            var result = graph.GetWeight(startLabel, finalLabel);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GraphAlgorithms_Question9_Success()
        {
            // Question 9: The length of the shortest route (in terms of distance to travel) from B to B.
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 9;

            // Act
            var startLabel = "B";
            var finalLabel = "B";
            var result = graph.GetWeight(startLabel, finalLabel);

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GraphAlgorithms_Question10_Success()
        {
            // Question 10: The number of different routes from C to C with a distance of less than 30.
            // Arrange
            var graph = GraphTestData.GetGraph();
            var expected = 7;

            // Act
            var startLabel = "C";
            var finalLabel = "C";
            var maxDistance = 30;
            var paths = graph.GetPathsWithinRange(startLabel, finalLabel, maxDistance);
            var result = paths.Count();

            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}

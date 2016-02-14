// ---------------------------------------------------------------------------------
// ICT - ICT.Collections.Tests
// PriorityQueueTests.cs
// DRNL
// 2016.02.08
// ---------------------------------------------------------------------------------

using ICT.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICT.Collections.Tests
{
    /// <summary>
    /// Summary description for PriorityQueueTests.
    /// </summary>
    [TestClass]
    public class PriorityQueueTests
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        [TestInitialize]
        public void TestInitialize()
        {
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
        public void PriorityQueue_Constructor_PriorityQueueNotNull_Success()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();

            // Act
            // ...

            // Assert
            Assert.IsNotNull(priorityQueue);
        }

        [TestMethod]
        public void PriorityQueue_Enqueue_NonRepeatedValues_Success()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();
            var values = new int[] { 10, 7, 6, 1, 2, 3, 5, 4, 9, 8 };
            var expected = "1,2,3,4,6,7,5,10,9,8";

            // Act
            foreach (var value in values)
            {
                priorityQueue.Enqueue(value);
            }

            var result = priorityQueue.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void PriorityQueue_Enqueue_RepeatedValues_Success()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();
            var values = new int[] { 10, 5, 6, 1, 2, 3, 6, 4, 5, 8 };
            var expected = "1,2,3,4,5,6,6,10,5,8";

            // Act
            foreach (var value in values)
            {
                priorityQueue.Enqueue(value);
            }

            var result = priorityQueue.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void PriorityQueue_Dequeue_NonRepeatedValues_Success()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();
            var values = new int[] { 10, 7, 6, 1, 2, 3, 5, 4, 9, 8 };
            var expected = "1,2,3,4,5,6,7,8,9,10";

            // Act
            foreach (var value in values)
            {
                priorityQueue.Enqueue(value);
            }

            var result = "";
            var index = 0;
            var lastIndex = priorityQueue.Count() - 1;

            while (!priorityQueue.IsEmpty())
            {
                var element = priorityQueue.Dequeue();

                result += index++ == lastIndex ?
                    string.Format("{0}", element.ToString()) : string.Format("{0},", element.ToString());
            }

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void PriorityQueue_Dequeue_RepeatedValues_Success()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();
            var values = new int[] { 10, 5, 6, 1, 2, 3, 6, 4, 5, 8 };
            var expected = "1,2,3,4,5,5,6,6,8,10";

            // Act
            foreach (var value in values)
            {
                priorityQueue.Enqueue(value);
            }

            var result = "";
            var index = 0;
            var lastIndex = priorityQueue.Count() - 1;

            while (!priorityQueue.IsEmpty())
            {
                var element = priorityQueue.Dequeue();

                result += index++ == lastIndex ?
                    string.Format("{0}", element.ToString()) : string.Format("{0},", element.ToString());
            }

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void PriorityQueue_Peek_EmptyCollection_ThrowsException()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();

            // Act
            priorityQueue.Peek();

            // Assert
            // ...
        }

        [TestMethod]
        [ExpectedException(typeof(CoreException))]
        public void PriorityQueue_Dequeue_EmptyCollection_ThrowsException()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();

            // Act
            priorityQueue.Dequeue();

            // Assert
            // ...
        }

        [TestMethod]
        public void PriorityQueue_ToString_NonEmptyCollection_Success()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();
            var values = new int[] { 10, 7, 6, 1, 2, 3, 5, 4, 9, 8 };
            var expected = "1,2,3,4,5,6,7,8,9,10";

            // Act
            foreach (var value in values)
            {
                priorityQueue.Enqueue(value);
            }

            var result = "";
            var index = 0;
            var lastIndex = priorityQueue.Count() - 1;

            while (!priorityQueue.IsEmpty())
            {
                var element = priorityQueue.Dequeue();

                result += index++ == lastIndex ?
                    string.Format("{0}", element.ToString()) : string.Format("{0},", element.ToString());
            }

            // Assert
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void PriorityQueue_ToString_EmptyCollection_Success()
        {
            // Arrange
            var priorityQueue = new PriorityQueue<int>();
            var expected = "";

            // Act
            var result = priorityQueue.ToString();

            // Assert
            Assert.AreEqual(result, expected);
        }
    }
}

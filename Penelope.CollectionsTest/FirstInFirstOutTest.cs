using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace Penelope.Collections.Test
{
    [TestClass]
    public class FirstInFirstOutTest
    {
        #region Test Constructors

        [TestMethod]
        public void TestFIFOConstructor1()
        {
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>(3, 4, null);

            Assert.AreEqual(0, fifo.Queue.Count);
        }

        [TestMethod]
        public void TestFIFOConstructor2()
        {
            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>(expected);

            ReadOnlyCollection<int> actual = fifo.Queue;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
            Assert.AreEqual(expected[5], actual[5]);
        }

        #endregion Test Constructors

        #region Tests Enqueue

        [TestMethod]
        public void TestEnqueue1()
        {
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>();
            Assert.AreEqual(0, fifo.Queue.Count);

            fifo.Enqueue(1);
            fifo.Enqueue(3);
            Assert.AreEqual(2, fifo.Queue.Count);

            fifo.Enqueue(5);
            Assert.AreEqual(3, fifo.Queue.Count);

            fifo.Enqueue(7);
            fifo.Enqueue(9);
            fifo.Enqueue(11);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            ReadOnlyCollection<int> actual = fifo.Queue;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
            Assert.AreEqual(expected[5], actual[5]);
        }

        [TestMethod]
        public void TestEnqueue2()
        {
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>();

            fifo.Enqueue(1);
            fifo.Enqueue(3);
            fifo.Enqueue(5);
            fifo.Enqueue(7);
            fifo.Enqueue(9);
            fifo.Enqueue(11);

            fifo.Remove(5);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 7, 9, 11 });
            ReadOnlyCollection<int> actual = fifo.Queue;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
        }

        [TestMethod]
        public void TestEnqueue3()
        {
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>();

            fifo.Enqueue(1);
            fifo.Enqueue(3);
            fifo.Enqueue(5);
            fifo.Enqueue(7);
            fifo.Enqueue(9);
            fifo.Enqueue(11);

            fifo.Remove(3);
            fifo.Remove(5);
            fifo.Remove(9);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 7, 11 });
            ReadOnlyCollection<int> actual = fifo.Queue;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }

        [TestMethod]
        public void TestEnqueue4()
        {
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>();

            fifo.Enqueue(1);
            fifo.Enqueue(3);
            fifo.Enqueue(5);
            fifo.Enqueue(7);
            fifo.Enqueue(9);
            fifo.Enqueue(11);

            fifo.Remove(3);
            fifo.Remove(5);
            fifo.Remove(9);

            fifo.Enqueue(6);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 7, 11, 6 });
            ReadOnlyCollection<int> actual = fifo.Queue;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void TestEnqueue5()
        {
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>();

            fifo.Enqueue(1);
            fifo.Enqueue(3);
            fifo.Enqueue(5);
            fifo.Enqueue(7);
            fifo.Enqueue(9);
            fifo.Enqueue(11);

            fifo.Remove(3);
            fifo.Enqueue(10);
            fifo.Remove(5);
            fifo.Enqueue(6);
            fifo.Remove(9);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 7, 11, 6, 10 });
            ReadOnlyCollection<int> actual = fifo.Queue;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);

            Assert.AreNotEqual(expected[3], actual[3]);
            Assert.AreNotEqual(expected[4], actual[4]);

            Assert.AreEqual(expected[4], actual[3]);
            Assert.AreEqual(expected[3], actual[4]);
        }

        #endregion Tests Enqueue

        #region Tests Dequeue

        [TestMethod]
        public void TestDequeue1()
        {
            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>(expected);

            Assert.AreEqual(expected[0], fifo.Dequeue());
            Assert.AreEqual(expected.Count - 1, fifo.Queue.Count);

            Assert.AreEqual(expected[1], fifo.Dequeue());
            Assert.AreEqual(expected[2], fifo.Dequeue());

            Assert.AreEqual(expected[3], fifo.Peek());
            Assert.AreEqual(expected[3], fifo.Dequeue());
            Assert.AreEqual(expected[4], fifo.Peek());
        }

        [TestMethod]
        public void TestDequeue2()
        {
            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            FirstInFirstOut<int> fifo = new FirstInFirstOut<int>(expected);

            Assert.AreEqual(expected[0], fifo.Dequeue());
            Assert.AreEqual(expected[1], fifo.Dequeue());
            fifo.Enqueue(10);
            Assert.AreEqual(expected[2], fifo.Dequeue());
            fifo.Enqueue(6);
            Assert.AreEqual(expected[3], fifo.Dequeue());

            expected = new ReadOnlyCollection<int>(new int[] { 9, 11, 10, 6 });
            ReadOnlyCollection<int> actual = fifo.Queue;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        #endregion Tests Dequeue
    }
}

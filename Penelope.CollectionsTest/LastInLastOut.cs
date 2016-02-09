using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace Penelope.Collections.Test
{
    [TestClass]
    public class LastInLastOutTest
    {
        #region Test Constructors

        [TestMethod]
        public void TestLastInLastOutConstructor1()
        {
            LastInFirstOut<int> lifo = new LastInFirstOut<int>(3, 4, null);

            Assert.AreEqual(0, lifo.Stack.Count);
        }

        [TestMethod]
        public void TestLastInLastOutConstructor2()
        {
            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            LastInFirstOut<int> lifo = new LastInFirstOut<int>(expected);

            ReadOnlyCollection<int> actual = lifo.Stack;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
            Assert.AreEqual(expected[5], actual[5]);
        }

        #endregion Test Constructors

        #region Tests Push

        [TestMethod]
        public void TestPush1()
        {
            LastInFirstOut<int> lifo = new LastInFirstOut<int>();
            Assert.AreEqual(0, lifo.Stack.Count);

            lifo.Push(1);
            lifo.Push(3);
            Assert.AreEqual(2, lifo.Stack.Count);

            lifo.Push(5);
            Assert.AreEqual(3, lifo.Stack.Count);

            lifo.Push(7);
            lifo.Push(9);
            lifo.Push(11);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            ReadOnlyCollection<int> actual = lifo.Stack;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
            Assert.AreEqual(expected[5], actual[5]);
        }

        [TestMethod]
        public void TestPush2()
        {
            LastInFirstOut<int> lifo = new LastInFirstOut<int>();

            lifo.Push(1);
            lifo.Push(3);
            lifo.Push(5);
            lifo.Push(7);
            lifo.Push(9);
            lifo.Push(11);

            lifo.Remove(5);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 7, 9, 11 });
            ReadOnlyCollection<int> actual = lifo.Stack;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
            Assert.AreEqual(expected[4], actual[4]);
        }

        [TestMethod]
        public void TestPush3()
        {
            LastInFirstOut<int> lifo = new LastInFirstOut<int>();

            lifo.Push(1);
            lifo.Push(3);
            lifo.Push(5);
            lifo.Push(7);
            lifo.Push(9);
            lifo.Push(11);

            lifo.Remove(3);
            lifo.Remove(5);
            lifo.Remove(9);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 7, 11 });
            ReadOnlyCollection<int> actual = lifo.Stack;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
        }

        [TestMethod]
        public void TestPush4()
        {
            LastInFirstOut<int> lifo = new LastInFirstOut<int>();

            lifo.Push(1);
            lifo.Push(3);
            lifo.Push(5);
            lifo.Push(7);
            lifo.Push(9);
            lifo.Push(11);

            lifo.Remove(3);
            lifo.Remove(5);
            lifo.Remove(9);

            lifo.Push(6);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 7, 11, 6 });
            ReadOnlyCollection<int> actual = lifo.Stack;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void TestPush5()
        {
            LastInFirstOut<int> lifo = new LastInFirstOut<int>();

            lifo.Push(1);
            lifo.Push(3);
            lifo.Push(5);
            lifo.Push(7);
            lifo.Push(9);
            lifo.Push(11);

            lifo.Remove(3);
            lifo.Push(10);
            lifo.Remove(5);
            lifo.Push(6);
            lifo.Remove(9);

            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 7, 11, 6, 10 });
            ReadOnlyCollection<int> actual = lifo.Stack;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);

            Assert.AreNotEqual(expected[3], actual[3]);
            Assert.AreNotEqual(expected[4], actual[4]);

            Assert.AreEqual(expected[4], actual[3]);
            Assert.AreEqual(expected[3], actual[4]);
        }

        #endregion Tests Push

        #region Tests Pop

        [TestMethod]
        public void TestPop1()
        {
            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            LastInFirstOut<int> lifo = new LastInFirstOut<int>(expected);

            Assert.AreEqual(expected[5], lifo.Pop());
            Assert.AreEqual(expected.Count - 1, lifo.Stack.Count);

            Assert.AreEqual(expected[4], lifo.Pop());
            Assert.AreEqual(expected[3], lifo.Pop());

            Assert.AreEqual(expected[2], lifo.Peek());
            Assert.AreEqual(expected[2], lifo.Pop());
            Assert.AreEqual(expected[1], lifo.Peek());
        }

        [TestMethod]
        public void TestPop2()
        {
            ReadOnlyCollection<int> expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7, 9, 11 });
            LastInFirstOut<int> lifo = new LastInFirstOut<int>(expected);

            Assert.AreEqual(expected[5], lifo.Pop());
            Assert.AreEqual(expected[4], lifo.Pop());
            lifo.Push(10);
            Assert.AreEqual(10, lifo.Pop());
            lifo.Push(6);
            Assert.AreEqual(6, lifo.Pop());

            expected = new ReadOnlyCollection<int>(new int[] { 1, 3, 5, 7 });
            ReadOnlyCollection<int> actual = lifo.Stack;
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        #endregion Tests Pop
    }
}

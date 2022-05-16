using NUnit.Framework;

using static MiniTests.SuspiciousFunctions;

namespace MiniTests
{
    public class SuspiciousFunctionsTests
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        public void TestFibonacci(int n, int expected)
        {
            Assert.AreEqual(expected, Fibonacci(n));
        }

        [TestCase(new[] { 0, 1, 2 }, 1)]
        [TestCase(new[] { 2, 1, 0 }, 1)]
        [TestCase(new[] { 2, 1, 1, 0 }, 1)]
        [TestCase(new[] { 1, 2, 2, 0 }, 1)]
        [TestCase(new[] { -1, -2, -3 }, -2)]
        [TestCase(new[] { -1, -1, -2, -3 }, -2)]
        public void TestViceMax(int[] array, int expected)
        {
            Assert.AreEqual(expected, ViceMax(array));
        }
    }
}
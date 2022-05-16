using NUnit.Framework;

using static MiniTests.EmptyFunctions;

namespace MiniTests
{
    public class EmptyFunctionsTests
    {
        [TestCase(12345, 1, 5)]
        [TestCase(12345, 5, 1)]
        [TestCase(10, 1, 0)]
        [TestCase(806, 2, 0)]
        [TestCase(0, 1, 0)]
        public void TestGetDigit(int n, int digit, int expected)
        {
            Assert.AreEqual(expected, GetDigit(n, digit));
        }

        [TestCase(12321, true)]
        [TestCase(123321, true)]
        [TestCase(55, true)]
        [TestCase(0, true)]
        [TestCase(5, true)]
        [TestCase(51, false)]
        [TestCase(50, false)]
        [TestCase(15, false)]
        [TestCase(153451, false)]
        public void TestIsNumberPalindrome(int n, bool expected)
        {
            Assert.AreEqual(expected, IsNumberPalindrome(n));
        }
    }
}
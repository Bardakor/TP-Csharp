using NUnit.Framework;
using SherlocksGambit.Game;
using SherlocksGambit.Utils.Encryption;

namespace SafetyNet
{
    [TestFixture]
    public class Threshold1
    {
        [Test] [Timeout(1000)]
        [TestCase("")]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2")]
        [TestCase("1m1m/m2m/mm2/mmmm/M3/MMMM/1MMM/1M1M")]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k")]
        public void TestFenEncrypt(string fenBoard)
        {
            // If `fenBoard` is empty, generate a starting fen. This is to accomodate the modular board bonus.
            fenBoard = string.IsNullOrEmpty(fenBoard) ? Board.GenerateStartingFen() : fenBoard; 
            Assert.AreEqual(fenBoard, FenEncryption.Encrypt(new Board(fenBoard)));
        }
    }
}
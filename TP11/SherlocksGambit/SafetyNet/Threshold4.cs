using System;
using System.Linq;
using NUnit.Framework;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils.Encryption;
using SherlocksGambit.Utils.Helpers;

namespace SafetyNet
{
    [TestFixture]
    public class Threshold4
    {
        [Test]
        [TestCase("", "(9-14)")]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2", "(2-7)")]
        [TestCase("1m1m/m2m/mm2/mmmm/M3/MMMM/1MMM/1M1M", "(9-14)")]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k", "(6-9)")]
        public void CheckEncrypt(string fenBoard, string expected)
        {
            var board = new Board(fenBoard);

            // Get the first pieces out of all of the ones on the board that can move
            var piece = board.PieceManager.PiecesDictionary[PlayerColor.Black]
                .Concat(board.PieceManager.PiecesDictionary[PlayerColor.White])
                .First(piece => piece.CanMove(false));

            // Get the first move that that piece could do and encode it
            var move = piece.CheckPathing(piece.CurrentCell.BoardPosition, null).First();
            var student = MoveEncryption.Encrypt(piece.Color, move);

            // Check if it is the move that we expected
            // `CheckPathing` is a function that WE implemented for you. It should thus always return the same move and
            // that, no matter how you implemented your AI
            Assert.AreEqual(expected, student);
        }

        [Test]
        [TestCase("")]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2")]
        [TestCase("1m1m/m2m/mm2/mmmm/M3/MMMM/1MMM/1M1M")]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k")]
        [TestCase(null)]
        public void CheckEncryptThenDecrypt(string fenBoard)
        {
            // If `fenBoard` is null, generate a starting fen. This is to accomodate the modular board bonus.
            var board = new Board(fenBoard ?? Board.GenerateStartingFen());
            var rand = new Random(DateTime.Now.Millisecond);

            // Get all the pieces that can move and select one at random
            var pieces = board.PieceManager.PiecesDictionary[PlayerColor.Black]
                .Concat(board.PieceManager.PiecesDictionary[PlayerColor.White])
                .Where(piece => piece.CanMove(false)).ToList();

            // If fenBoard is null, generate a random (and hopefully wrong) move
            // This test should never return true because a capture move at the start of the game is impossible
            if (fenBoard is null)
            {
                Assert.IsFalse(MoveEncryption.Decrypt(board,
                    $"{pieces[rand.Next(pieces.Count)].CurrentCell.BoardPosition}" + "x" + 
                    $"{pieces[rand.Next(pieces.Count)].CurrentCell.BoardPosition}"));
                
                // Testing the edge case of the first move
                Assert.IsTrue(MoveEncryption.Decrypt(board, null));
                return;
            }

            var piece = pieces[rand.Next(pieces.Count)];

            // Get all the possible move that randomly selected piece could do and select one at random
            var possibleMoves = piece.CheckPathing(piece.CurrentCell.BoardPosition, null);
            var move = possibleMoves[rand.Next(possibleMoves.Count)];

            var copy = board.Copy();

            // Encrypt the move and execute it
            var code = MoveEncryption.Encrypt(piece.Color, move);
            var valid = piece.Move(move);

            // Decrypt the move and check if the return value from the execution is what we expected it to be
            var decryptValid = MoveEncryption.Decrypt(copy, code);
            Assert.AreEqual(valid, decryptValid);

            // Check if after the move, the boards are identical (which they should be)
            Assert.AreEqual(FenEncryption.Encrypt(board), FenEncryption.Encrypt(copy));
        }
    }
}
using System;
using System.Linq;
using NUnit.Framework;
using SherlocksGambit.AI;
using SherlocksGambit.Game;
using SherlocksGambit.Game.Pieces;
using SherlocksGambit.Utils;
using SherlocksGambit.Utils.Encryption;
using SherlocksGambit.Utils.Helpers;

namespace SafetyNet
{
    [TestFixture]
    public class Threshold2
    {
        [Test]
        [Timeout(1000)]
        [TestCase("", false, PlayerColor.Black)]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2", false, PlayerColor.Black)]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k", false, PlayerColor.White)]
        [TestCase("4/K3/4/4/4/4/3K/4", true, PlayerColor.Black)]
        [TestCase("4/K3/4/4/4/4/3K/4", false, PlayerColor.White)]
        [TestCase("2mm/3M/4/1m2/1km1/4/m3/4", false, PlayerColor.Black)]
        [TestCase("2mm/3M/4/1m2/1km1/4/m3/4", false, PlayerColor.White)]
        public void TestHasWon(string fenBoard, bool expected, PlayerColor playerColor)
        {
            Assert.AreEqual(expected, new Board(fenBoard).HasWon(playerColor));
        }

        [Test]
        [Timeout(1000)]
        [TestCase("", false, PlayerColor.Black)]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2", false, PlayerColor.Black)]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k", false, PlayerColor.White)]
        [TestCase("4/K3/4/4/4/4/3K/4", false, PlayerColor.Black)]
        [TestCase("4/K3/4/4/4/4/3K/4", true, PlayerColor.White)]
        [TestCase("2mm/3M/4/1m2/1km1/4/m3/4", true, PlayerColor.Black)]
        [TestCase("2mm/3M/4/1m2/1km1/4/m3/4", false, PlayerColor.White)]
        public void TestHasLost(string fenBoard, bool expected, PlayerColor playerColor)
        {
            Assert.AreEqual(expected, new Board(fenBoard).HasLost(playerColor));
        }

        [Test]
        [Timeout(1000)]
        [TestCase("", false, PlayerColor.Black)]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2", false, PlayerColor.Black)]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k", false, PlayerColor.White)]
        [TestCase("4/K3/4/4/4/4/3K/4", true, PlayerColor.Black)]
        [TestCase("4/K3/4/4/4/4/3K/4", true, PlayerColor.White)]
        [TestCase("2mm/3M/4/1m2/1km1/4/m3/4", true, PlayerColor.Black)]
        [TestCase("2mm/3M/4/1m2/1km1/4/m3/4", false, PlayerColor.White)]
        public void TestIsEoG(string fenBoard, bool expected, PlayerColor playerColor)
        {
            var board = new Board(fenBoard);
            var brain = new AiProperties((int)playerColor, board, 0, null);
            Assert.AreEqual(expected, new MiniMax(brain).IsEoG(board));
        }

        [Test]
        [Timeout(1000)]
        public void TestManCopy()
        {
            var blackMan = new Man(PlayerColor.White);
            Assert.AreEqual(blackMan.Color, blackMan.Copy().Color);

            var whiteMan = new Man(PlayerColor.White);
            Assert.AreEqual(whiteMan.Color, whiteMan.Copy().Color);
        }

        [Test]
        [Timeout(1000)]
        public void TestKingCopy()
        {
            var blackMan = new King(PlayerColor.White);
            Assert.AreEqual(blackMan.Color, blackMan.Copy().Color);

            var whiteMan = new King(PlayerColor.White);
            Assert.AreEqual(whiteMan.Color, whiteMan.Copy().Color);
        }

        [Test]
        [Timeout(1000)]
        [TestCase("")]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2")]
        [TestCase("1m1m/m2m/mm2/mmmm/M3/MMMM/1MMM/1M1M")]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k")]
        public void TestBoardCopy(string fenBoard)
        {
            var board = new Board(fenBoard);
            var copy = board.Copy();

            // Making sure the copy has the same amount of cells and that it has its own PieceManager
            Assert.AreEqual(board.Positions.Length, copy.Positions.Length);
            Assert.IsFalse(ReferenceEquals(board.PieceManager, copy.PieceManager));

            foreach (var (key, list) in copy.PieceManager.PiecesDictionary)
            {
                Assert.IsTrue(board.PieceManager.PiecesDictionary.ContainsKey(key));
                Assert.IsFalse(ReferenceEquals(list, board.PieceManager.PiecesDictionary[key]));
                Assert.AreEqual(board.PieceManager.PiecesDictionary[key].Count, list.Count,
                    $"Got {list.Count}, expected {board.PieceManager.PiecesDictionary[key].Count}, " +
                    $"please make sure that you added your pieces to the piece manager");
            }

            for (var i = 0; i < board.Positions.Length; i++)
            {
                // Making sure the copied board is made of new cells
                Assert.IsFalse(ReferenceEquals(board.Positions[i], copy.Positions[i]),
                    "Same reference, you did not clone the positions properly");

                // If there were no piece on this cell, let there be no piece either on the copied board
                if (board.Positions[i].CurrentPiece is null)
                    Assert.AreEqual(null, copy.Positions[i].CurrentPiece, "Expected no piece but got one in the copy");
                // Else we make sure the piece is not the same and that it has the same type and color as the old one
                else
                {
                    Assert.IsFalse(ReferenceEquals(board.Positions[i].CurrentPiece, copy.Positions[i].CurrentPiece),
                        "Same reference, you did not clone the pieces properly");
                    Assert.AreEqual(board.Positions[i].CurrentPiece.GetType(),
                        copy.Positions[i].CurrentPiece.GetType(),
                        $"Got piece type {copy.Positions[i].CurrentPiece.GetType()}, expected a {board.Positions[i].CurrentPiece.GetType()}.");
                    Assert.AreEqual(board.Positions[i].CurrentPiece.Color, board.Positions[i].CurrentPiece.Color);
                }

                // Finally, just to be sure, let us check that the board positions are set correctly
                Assert.AreEqual(board.Positions[i].BoardPosition, copy.Positions[i].BoardPosition);
            }
        }

        [Test]
        [Timeout(1000)]
        [TestCase("")]
        [TestCase("1m2/m2m/mK1m/m1km/M1M1/1KKK/mM1M/1M2")]
        [TestCase("1m1m/m2m/mm2/mmmm/M3/MMMM/1MMM/1M1M")]
        [TestCase("4/4/4/1m2/1M2/1M1m/1M1M/3k")]
        public void TestTranslatePathObject(string fenBoard)
        {
            var board = new Board(fenBoard);

            // Get all the pieces that can move and select one at random
            var pieces = board.PieceManager.PiecesDictionary[PlayerColor.Black]
                .Concat(board.PieceManager.PiecesDictionary[PlayerColor.White])
                .Where(piece => piece.CanMove(false)).ToList();
            
            var piece = pieces[new Random(DateTime.Now.Millisecond).Next(pieces.Count)];

            // Get all the possible move that randomly selected piece could do and select one at random
            var possibleMoves = piece.CheckPathing(piece.CurrentCell.BoardPosition, null);
            var move = possibleMoves[new Random(DateTime.Now.Millisecond).Next(possibleMoves.Count)];

            // Copy the board and translate the move
            var copy = board.Copy();
            var newMove = move.Translate(copy);

            // Move both pieces on their respective board
            piece.Move(move);
            newMove.Path.First().CurrentPiece.Move(newMove);

            // Check if after the move, the boards are identical (which they should be)
            Assert.AreEqual(FenEncryption.Encrypt(board), FenEncryption.Encrypt(copy));
        }
    }
}
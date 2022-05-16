using NUnit.Framework;
using SudokuSolver;

namespace SudokuSolverTests
{
    public class SudokuTests
    {
        [Test]
        public void TestSudokuConstructor()
        {
            int[,] board = new int[9,9];
            Sudoku sudoku = new Sudoku(board);
            
            Assert.AreEqual(board, sudoku.Board);
        }
        
        [Test]
        public void TestSudokuGeneratorConstructor()
        {
            // TODO
        }
        
        [Test]
        public void TestSudokuIsSolved()
        {
            // TODO
        }
        
        [Test]
        public void TestSudokuIsBoardValid()
        {
            // TODO
        }
        
        [Test]
        public void TestSudokuSolve([Range(18, 80)] int difficulty)
        {
            Sudoku sudoku = new Sudoku(difficulty);
            sudoku.Solve();
            
            Assert.IsTrue(sudoku.IsSolved());
        }
    }
}
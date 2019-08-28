using engine.Game;
using NUnit.Framework;

namespace engine_tests
{
    public class ChessBoardTests
    {
        [TestCase(0, 0, true)]
        [TestCase(7, 0, true)]
        [TestCase(0, 7, true)]
        [TestCase(7, 7, true)]
        [TestCase(-1, 0, false)]
        [TestCase(0, -1, false)]
        [TestCase(8, 0, false)]
        [TestCase(0, 8, false)]
        public void IsValidPosition_ReturnsCorrectly(int row, int col, bool expected)
        {
            var board = new ChessBoard();
            var position = new Position(row, col);
            
            Assert.That(board.IsValidPosition(position), Is.EqualTo(expected));
        }
    }
}
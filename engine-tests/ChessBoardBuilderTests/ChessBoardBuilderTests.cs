using engine;
using engine.Pieces;
using NUnit.Framework;

namespace engine_tests
{
    public class ChessBoardBuilderTests
    {
        [Test]
        public void Build_ReturnsKingsInTheCorrectPositions()
        {
            var blackKing = new King("black");
            var whiteKing = new King("white");

            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            Assert.That(actualChessBoard.ChessPieces[0, 4].Equals(blackKing));
            Assert.That(actualChessBoard.ChessPieces[7, 4].Equals(whiteKing));
        }

        [Test]
        public void Build_ReturnsQueensInTheCorrectPositions()
        {
            var blackQueen = new Queen("black");
            var whiteQueen = new Queen("white");

            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            Assert.That(actualChessBoard.ChessPieces[0, 3].Equals(blackQueen));
            Assert.That(actualChessBoard.ChessPieces[7, 3].Equals(whiteQueen));
        }

        [Test]
        public void Build_ReturnsRooksInTheCorrectPositions()
        {
            var blackRook = new Rook("black");
            var whiteRook = new Rook("white");

            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            Assert.That(actualChessBoard.ChessPieces[0, 0].Equals(blackRook));
            Assert.That(actualChessBoard.ChessPieces[0, 7].Equals(blackRook));
            Assert.That(actualChessBoard.ChessPieces[7, 0].Equals(whiteRook));
            Assert.That(actualChessBoard.ChessPieces[7, 7].Equals(whiteRook));
        }

        [Test]
        public void Build_ReturnsBishopsInTheCorrectPositions()
        {
            var blackBishop = new Bishop("black");
            var whiteBishop = new Bishop("white");

            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            Assert.That(actualChessBoard.ChessPieces[0, 2].Equals(blackBishop));
            Assert.That(actualChessBoard.ChessPieces[0, 5].Equals(blackBishop));
            Assert.That(actualChessBoard.ChessPieces[7, 2].Equals(whiteBishop));
            Assert.That(actualChessBoard.ChessPieces[7, 5].Equals(whiteBishop));
        }

        [Test]
        public void Build_ReturnsKnightsInTheCorrectPositions()
        {
            var blackKnight = new Knight("black");
            var whiteKnight = new Knight("white");

            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            Assert.That(actualChessBoard.ChessPieces[0, 1].Equals(blackKnight));
            Assert.That(actualChessBoard.ChessPieces[0, 6].Equals(blackKnight));
            Assert.That(actualChessBoard.ChessPieces[7, 1].Equals(whiteKnight));
            Assert.That(actualChessBoard.ChessPieces[7, 6].Equals(whiteKnight));
        }

        [Test]
        public void Build_ReturnsPawnsInTheCorrectPositions()
        {
            var blackPawn = new Pawn("black");
            var whitePawn = new Pawn("white");

            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            for (var i = 0; i < 8; i++)
            {
                Assert.That(actualChessBoard.ChessPieces[1, i].Equals(blackPawn));
                Assert.That(actualChessBoard.ChessPieces[6, i].Equals(whitePawn));
            }
        }

        [Test]
        public void Build_ReturnsEmptyInTheCorrectPositions()
        {
            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            for (var row = 2; row < 6; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    Assert.That(actualChessBoard.ChessPieces[row, col], Is.Null);
                }
            }
        }
    }
}

using engine;
using engine.Board;
using engine.Pieces;
using NUnit.Framework;

namespace engine_tests
{
    public class ChessBoardBuilderTests
    {
        [Test]
        public void Build_ReturnsCorrectStartingBoard()
        {
            const string black = "black";
            const string white = "white";
            var expectedChessBoard = new ChessBoard
            {
                ChessPieces = new ChessPiece[,]
                {
                    {new Rook(black), new Knight(black), new Bishop(black), new Queen(black), new King(black), new Bishop(black), new Knight(black), new Rook(black)},
                    {new Pawn(black), new Pawn(black), new Pawn(black), new Pawn(black), new Pawn(black), new Pawn(black), new Pawn(black), new Pawn(black)},
                    {null, null, null, null, null, null, null, null},
                    {null, null, null, null, null, null, null, null},
                    {null, null, null, null, null, null, null, null},
                    {null, null, null, null, null, null, null, null},
                    {new Pawn(white), new Pawn(white), new Pawn(white), new Pawn(white), new Pawn(white), new Pawn(white), new Pawn(white), new Pawn(white)},
                    {new Rook(white), new Knight(white), new Bishop(white), new Queen(white), new King(white), new Bishop(white), new Knight(white), new Rook(white)}
                },
                ErrorMessage = null
            };

            var engine = new ChessEngine();
            var actualChessBoard = engine.GetNewChessBoard();

            Assert.AreEqual(expectedChessBoard, actualChessBoard);
        }
    }
}

using engine;
using engine.Game;
using engine.Pieces;
using NUnit.Framework;

namespace engine_tests
{
    [TestFixture]
    public class ChessBoardBuilderTests
    {
        [Test]
        public void Build_ReturnsCorrectStartingBoard()
        {
            var chessPieces = new ChessPiece[,]
            {
                {new Rook(Player.Black), new Knight(Player.Black), new Bishop(Player.Black), new Queen(Player.Black), new King(Player.Black), new Bishop(Player.Black), new Knight(Player.Black), new Rook(Player.Black)},
                {new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black), new Pawn(Player.Black)},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White), new Pawn(Player.White)},
                {new Rook(Player.White), new Knight(Player.White), new Bishop(Player.White), new Queen(Player.White), new King(Player.White), new Bishop(Player.White), new Knight(Player.White), new Rook(Player.White)}
            };
            var expectedChessBoard = new ChessBoard(chessPieces);

            var engine = new ChessEngine();
            var actualChessGame = engine.GetNewChessGame();

            Assert.AreEqual(expectedChessBoard.GetBoard(), actualChessGame.Board);
        }
    }
}
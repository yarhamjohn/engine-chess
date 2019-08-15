using engine;
using engine.Board;
using engine.Pieces;
using NUnit.Framework;

namespace engine_tests
{
    public class ChessEngineTests
    {
        const string black = "black";
        const string white = "white";
        private ChessBoard startingChessBoard;

        [SetUp]
        public void Setup()
        {
            startingChessBoard = new ChessBoard
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
        }
        
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_GivenSameStartingAndTargetPositions()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 0, Row = 1};
            var targetPosition = new Position { Column = 0, Row = 1};
            var newChessBoard = engine.MoveChessPiece(startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual(newChessBoard.ErrorMessage, "The starting position (Row: 1, Col: 0) was the same as the target position (Row: 1, Col: 0)");
            Assert.AreEqual(startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfStartingPositionIsOffTheBoard()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = -1, Row = 8};
            var targetPosition = new Position { Column = 0, Row = 0};
            var newChessBoard = engine.MoveChessPiece(startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual(newChessBoard.ErrorMessage, "The starting position (Row: 8, Col: -1) is not on the board");
            Assert.AreEqual(startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfTargetPositionIsOffTheBoard()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 0, Row = 0};
            var targetPosition = new Position { Column = 8, Row = -1};
            var newChessBoard = engine.MoveChessPiece(startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual(newChessBoard.ErrorMessage, "The targetPosition (Row: -1, Col: 8) is not on the board");
            Assert.AreEqual(startingChessBoard, newChessBoard);
        }   
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfNoPieceInSourcePosition()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 0, Row = 2};
            var targetPosition = new Position { Column = 0, Row = 3};
            var newChessBoard = engine.MoveChessPiece(startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual(newChessBoard.ErrorMessage, "There is no piece in the starting position (Row: 2, Col: 0)");
            Assert.AreEqual(startingChessBoard, newChessBoard);
        }
    }
}
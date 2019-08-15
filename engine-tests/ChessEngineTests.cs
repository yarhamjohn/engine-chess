using engine;
using engine.Board;
using engine.Pieces;
using NUnit.Framework;

namespace engine_tests
{
    public class ChessEngineTests
    {
        private const string Black = "black";
        private const string White = "white";
        private ChessBoard _startingChessBoard;

        [SetUp]
        public void Setup()
        {
            _startingChessBoard = new ChessBoard
            {
                ChessPieces = new ChessPiece[,]
                {
                    {new Rook(Black), new Knight(Black), new Bishop(Black), new Queen(Black), new King(Black), new Bishop(Black), new Knight(Black), new Rook(Black)},
                    {new Pawn(Black), new Pawn(Black), new Pawn(Black), new Pawn(Black), new Pawn(Black), new Pawn(Black), new Pawn(Black), new Pawn(Black)},
                    {null, null, null, null, null, null, null, null},
                    {null, null, null, null, null, null, null, null},
                    {null, null, null, null, null, null, null, null},
                    {null, null, null, null, null, null, null, null},
                    {new Pawn(White), new Pawn(White), new Pawn(White), new Pawn(White), new Pawn(White), new Pawn(White), new Pawn(White), new Pawn(White)},
                    {new Rook(White), new Knight(White), new Bishop(White), new Queen(White), new King(White), new Bishop(White), new Knight(White), new Rook(White)}
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
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("The starting position (Row: 1, Col: 0) was the same as the target position (Row: 1, Col: 0)", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfStartingPositionIsOffTheBoard()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = -1, Row = 8};
            var targetPosition = new Position { Column = 0, Row = 0};
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("The starting position (Row: 8, Col: -1) is not on the board", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfTargetPositionIsOffTheBoard()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 0, Row = 0};
            var targetPosition = new Position { Column = 8, Row = -1};
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("The targetPosition (Row: -1, Col: 8) is not on the board", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }   
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfNoPieceInSourcePosition()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 0, Row = 2};
            var targetPosition = new Position { Column = 0, Row = 3};
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("There is no piece in the starting position (Row: 2, Col: 0)", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }

        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsNotAValidNormalMoveOrSpecialMove()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 0, Row = 1};
            var targetPosition = new Position { Column = 0, Row = 4};
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This move (Rows: 3, Cols: 0) is not valid for this piece (black Pawn). " +
                                                "Valid normal moves are: (1, 0), (1, 1), (1, -1). " +
                                                "Valid special moves are: (2, 0).";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }

        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidPawnTwoPlaceMove_ButThePawnHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 0, Row = 1};
            var targetPosition = new Position { Column = 0, Row = 3};
            
            _startingChessBoard.ChessPieces[startingPosition.Row, startingPosition.Column].MarkAsMoved();
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 2, Cols: 0) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidCastlingMove_ButTheKingHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 4, Row = 0};
            var targetPosition = new Position { Column = 2, Row = 0};
            
            _startingChessBoard.ChessPieces[startingPosition.Row, startingPosition.Column].MarkAsMoved();
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 0, Cols: -2) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidCastlingMove_ButTheCastleHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position { Column = 4, Row = 0};
            var targetPosition = new Position { Column = 6, Row = 0};

            var castlePosition = new Position() {Column = 7, Row = 0};
            _startingChessBoard.ChessPieces[castlePosition.Row, castlePosition.Column].MarkAsMoved();
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 0, Cols: 2) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
    }
}
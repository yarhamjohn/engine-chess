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
        private ChessBoard _castlingChessBoard;

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
            
            _castlingChessBoard = new ChessBoard
            {
                ChessPieces = new ChessPiece[,]
                {
                    {new Rook(Black), null, null, null, new King(Black), null, null, new Rook(Black)},
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
            var startingPosition = new Position(1, 0);
            var targetPosition = new Position(1, 0);
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("The starting position (Row: 1, Col: 0) was the same as the target position (Row: 1, Col: 0)", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfStartingPositionIsOffTheBoard()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(8, -1);
            var targetPosition = new Position(0, 0);
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("The starting position (Row: 8, Col: -1) is not on the board", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfTargetPositionIsOffTheBoard()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(-1, 8);
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("The targetPosition (Row: -1, Col: 8) is not on the board", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }   
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfNoPieceInSourcePosition()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(2, 0);
            var targetPosition = new Position(3, 0);
            
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            Assert.AreEqual("There is no piece in the starting position (Row: 2, Col: 0)", newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }

        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsNotAValidNormalMoveOrSpecialMove()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(1, 0);
            var targetPosition = new Position(4, 0);
            
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
            var startingPosition = new Position(1, 0);
            var targetPosition = new Position(3, 0);
            
            _startingChessBoard.ChessPieces[startingPosition.Row, startingPosition.Column].MarkAsMoved();
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 2, Cols: 0) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidCastlingMove_ButTheMoveIsBlocked()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 4);
            var targetPosition = new Position(0, 6);

            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 0, Cols: 2) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidCastlingMove_ButTheKingHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 4);
            var targetPosition = new Position(0, 2);

            _castlingChessBoard.ChessPieces[startingPosition.Row, startingPosition.Column].MarkAsMoved();
            var newChessBoard = engine.MoveChessPiece(_castlingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 0, Cols: -2) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_castlingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidCastlingMove_ButTheRookHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 4);
            var targetPosition = new Position(0, 6);
            var rookPosition = new Position(0, 7);
            
            _castlingChessBoard.ChessPieces[rookPosition.Row, rookPosition.Column].MarkAsMoved();
            
            var newChessBoard = engine.MoveChessPiece(_castlingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 0, Cols: 2) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_castlingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidCastlingMove_ButThereIsNoPieceInTheRookPosition()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 4);
            var targetPosition = new Position(0, 6);
            var rookPosition = new Position(0, 7);

            _castlingChessBoard.ChessPieces[rookPosition.Row, rookPosition.Column] = null;
            
            var newChessBoard = engine.MoveChessPiece(_castlingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 0, Cols: 2) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_castlingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfMoveIsAValidCastlingMove_ButThereIsADifferentPieceInTheRookPosition()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 4);
            var targetPosition = new Position(0, 2);
            var rookPosition = new Position(0, 0);

            _castlingChessBoard.ChessPieces[rookPosition.Row, rookPosition.Column] = new Queen("black");
            
            var newChessBoard = engine.MoveChessPiece(_castlingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This special move (Rows: 0, Cols: -2) is not valid at this time.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_castlingChessBoard, newChessBoard);
        }

        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfTargetPositionContainsPieceFromTheSameTeam()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 1);
            var targetPosition = new Position(2, 2);
            
            _startingChessBoard.ChessPieces[targetPosition.Row, targetPosition.Column] = new Pawn("black");

            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This move (Rows: 2, Cols: 1) is blocked by another piece.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfPawnIsMovedStraightOnePlaceAndTargetPositionIsOccupied()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(1, 2);
            var targetPosition = new Position(2, 2);

            _startingChessBoard.ChessPieces[targetPosition.Row, targetPosition.Column] = new Bishop("white");

            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This move (Rows: 1, Cols: 0) is blocked by another piece.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfPawnIsMovedStraightTwoPlacesAndTargetPositionIsOccupied()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(1, 2);
            var targetPosition = new Position(3, 2);

            _startingChessBoard.ChessPieces[targetPosition.Row, targetPosition.Column] = new Bishop("white");

            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This move (Rows: 2, Cols: 0) is blocked by another piece.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfAPieceFromTheSameTeamOccupiesAPositionBetweenSourceAndTarget()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(2, 5);
            var targetPosition = new Position(5, 2);
            var blockingPosition = new Position(4, 3);

            _startingChessBoard.ChessPieces[startingPosition.Row, startingPosition.Column] = new Queen("black");
            _startingChessBoard.ChessPieces[targetPosition.Row, targetPosition.Column] = new Rook("white");
            _startingChessBoard.ChessPieces[blockingPosition.Row, blockingPosition.Column] = new Knight("black");
            
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This move (Rows: 2, Cols: 0) is blocked by another piece.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
        
        [Test]
        public void MoveChessPiece_ReturnsSameBoardWithErrorMessage_IfAPieceFromTheOpposingTeamOccupiesAPositionBetweenSourceAndTarget()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(2, 5);
            var targetPosition = new Position(5, 2);
            var blockingPosition = new Position(4, 3);

            _startingChessBoard.ChessPieces[startingPosition.Row, startingPosition.Column] = new Queen("black");
            _startingChessBoard.ChessPieces[targetPosition.Row, targetPosition.Column] = new Rook("white");
            _startingChessBoard.ChessPieces[blockingPosition.Row, blockingPosition.Column] = new Knight("white");
            
            var newChessBoard = engine.MoveChessPiece(_startingChessBoard, startingPosition, targetPosition);

            const string expectedErrorMessage = "This move (Rows: 2, Cols: 0) is blocked by another piece.";
            Assert.AreEqual(expectedErrorMessage, newChessBoard.ErrorMessage);
            Assert.AreEqual(_startingChessBoard, newChessBoard);
        }
    }
}
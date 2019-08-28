using System;
using engine;
using engine.Game;
using engine.Pieces;
using NUnit.Framework;

namespace engine_tests
{
    [TestFixture]
    public class ChessEngineTests
    {
        private ChessBoard _emptyChessBoard;

        [SetUp]
        public void Setup()
        {
            _emptyChessBoard = new ChessBoard();
        }

        [Test]
        public void MoveChessPieceThrows_GivenMatchingStartAndTargetPositions()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(0, 0);

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "The starting position (Row: 0, Col: 0) was the same as the target position (Row: 0, Col: 0)"),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenStartPositionOffTheBoard()
        {
            var engine = new ChessEngine();
            
            var startingPosition = new Position(0, 8);
            var targetPosition = new Position(0, 0);

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "The starting position (Row: 0, Col: 8) is not on the board"),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenTargetPositionOffTheBoard()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(-1, 0);

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "The target position (Row: -1, Col: 0) is not on the board"),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenNoPieceInStartPosition()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(1, 0);

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "There is no piece in the starting position (Row: 0, Col: 0)"),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenInvalidMovement()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(7, 7);

            _emptyChessBoard.SetPosition(startingPosition, new Pawn(Player.Black));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 7, Cols: 7) is not valid for this piece (Black Pawn)."),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenPawnSpecialMove_WhenPawnHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(2, 0);

            _emptyChessBoard.SetPosition(startingPosition, new Pawn(Player.Black));
            _emptyChessBoard.GetPiece(startingPosition).MarkAsMoved();

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 2, Cols: 0) is not valid for this piece (Black Pawn)."),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void MoveChessPieceThrows_GivenCastlingMove_WhenMoveIsBlocked(int col)
        {
            var engine = new ChessEngine();
            var kingStartingPosition = new Position(0, 4);
            var kingTargetPosition = new Position(0, 2);
            var blockingPosition = new Position(0, col);
            var rookPosition = new Position(0, 0);

            _emptyChessBoard.SetPosition(kingStartingPosition, new King(Player.Black));
            _emptyChessBoard.SetPosition(rookPosition, new Rook(Player.Black));
            _emptyChessBoard.SetPosition(blockingPosition, new Bishop(Player.White));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 0, Cols: -2) is not valid for this piece (Black King)."),
                () => engine.MoveChessPiece(_emptyChessBoard, kingStartingPosition, kingTargetPosition));
        }
//        
//        [Test]
//        public void MoveChessPieceThrows_GivenCastlingMove_WhenKingMovesOverAttackedPosition()
//        {
//            var engine = new ChessEngine();
//            var kingStartingPosition = new Position(0, 4);
//            var kingTargetPosition = new Position(0, 2);
//            var attackingPosition= new Position(7, 3);
//            var rookPosition = new Position(0, 0);
//
//            _emptyChessBoard.SetPosition(kingStartingPosition, new King(Players.Black));
//            _emptyChessBoard.SetPosition(rookPosition, new Rook(Players.Black));
//            _emptyChessBoard.SetPosition(attackingPosition, new Queen(Players.White));
//
//            Assert.Throws(
//                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
//                    "This move (Rows: 0, Cols: -2) is not valid for this piece (Black King)."),
//                () => engine.MoveChessPiece(_emptyChessBoard, kingStartingPosition, kingTargetPosition));
//        }
        
        [Test]
        public void MoveChessPieceThrows_GivenCastlingMove_WhenKingHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var kingStartingPosition = new Position(0, 4);
            var kingTargetPosition = new Position(0, 2);
            var rookPosition = new Position(0, 0);

            _emptyChessBoard.SetPosition(kingStartingPosition, new King(Player.Black));
            _emptyChessBoard.SetPosition(rookPosition, new Rook(Player.Black));
            _emptyChessBoard.GetPiece(kingStartingPosition).MarkAsMoved();

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 0, Cols: -2) is not valid for this piece (Black King)."),
                () => engine.MoveChessPiece(_emptyChessBoard, kingStartingPosition, kingTargetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenCastlingMove_WhenRookHasAlreadyMoved()
        {
            var engine = new ChessEngine();
            var kingStartingPosition = new Position(0, 4);
            var kingTargetPosition = new Position(0, 2);
            var rookPosition = new Position(0, 0);

            _emptyChessBoard.SetPosition(kingStartingPosition, new King(Player.Black));
            _emptyChessBoard.SetPosition(rookPosition, new Rook(Player.Black));
            _emptyChessBoard.GetPiece(rookPosition).MarkAsMoved();

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 0, Cols: -2) is not valid for this piece (Black King)."),
                () => engine.MoveChessPiece(_emptyChessBoard, kingStartingPosition, kingTargetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenCastlingMove_WhenRookPositionIsEmpty()
        {
            var engine = new ChessEngine();
            var kingStartingPosition = new Position(0, 4);
            var kingTargetPosition = new Position(0, 2);

            _emptyChessBoard.SetPosition(kingStartingPosition, new King(Player.Black));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 0, Cols: -2) is not valid for this piece (Black King)."),
                () => engine.MoveChessPiece(_emptyChessBoard, kingStartingPosition, kingTargetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenCastlingMove_WhenRookPositionContainsNonRook()
        {
            var engine = new ChessEngine();
            var kingStartingPosition = new Position(0, 4);
            var kingTargetPosition = new Position(0, 2);
            var rookPosition = new Position(0, 0);

            _emptyChessBoard.SetPosition(kingStartingPosition, new King(Player.Black));
            _emptyChessBoard.SetPosition(rookPosition, new Queen(Player.Black));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 0, Cols: -2) is not valid for this piece (Black King)."),
                () => engine.MoveChessPiece(_emptyChessBoard, kingStartingPosition, kingTargetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenTargetPositionOccupiedByTheSameTeam()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(1, 0);

            _emptyChessBoard.SetPosition(startingPosition, new Rook(Player.Black));
            _emptyChessBoard.SetPosition(targetPosition, new Pawn(Player.Black));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 1, Cols: 0) is blocked by another piece."),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenNormalPawnForwardMove_WhenTargetPositionIsOccupiedByOpponent()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(1, 0);

            _emptyChessBoard.SetPosition(startingPosition, new Pawn(Player.Black));
            _emptyChessBoard.SetPosition(targetPosition, new Bishop(Player.White));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 1, Cols: 0) is blocked by another piece."),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenSpecialPawnForwardMove_WhenTargetPositionIsOccupiedByOpponent()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(2, 0);

            _emptyChessBoard.SetPosition(startingPosition, new Pawn(Player.Black));
            _emptyChessBoard.SetPosition(targetPosition, new Bishop(Player.White));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 2, Cols: 0) is blocked by another piece."),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenMoveBlockedBySameTeam()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(2, 0);
            var blockingPosition = new Position(1, 0);

            _emptyChessBoard.SetPosition(startingPosition, new Rook(Player.Black));
            _emptyChessBoard.SetPosition(targetPosition, new Queen(Player.White));
            _emptyChessBoard.SetPosition(blockingPosition, new Knight(Player.Black));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 2, Cols: 0) is blocked by another piece."),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }

        [Test]
        public void MoveChessPieceThrows_GivenMoveBlockedByOpponent()
        {
            var engine = new ChessEngine();
            var startingPosition = new Position(0, 0);
            var targetPosition = new Position(2, 0);
            var blockingPosition = new Position(1, 0);

            _emptyChessBoard.SetPosition(startingPosition, new Rook(Player.Black));
            _emptyChessBoard.SetPosition(targetPosition, new Queen(Player.White));
            _emptyChessBoard.SetPosition(blockingPosition, new Knight(Player.White));

            Assert.Throws(
                Is.TypeOf<InvalidOperationException>().And.Message.EqualTo(
                    "This move (Rows: 2, Cols: 0) is blocked by another piece."),
                () => engine.MoveChessPiece(_emptyChessBoard, startingPosition, targetPosition));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using engine.Game;
using engine.Pieces;

namespace engine
{
    public class ChessEngine
    {
        public ChessGame GetNewChessGame()
        {
            var builder = new ChessBoardBuilder();
            return new ChessGame
            {
                Board = builder.Build().GetBoard(),
                ActivePlayer = "white",
                GameState = ""
            };
        }

        public ChessGame MoveChessPiece(ChessBoard chessBoard, Position startPosition, Position targetPosition)
        {
            if (!chessBoard.IsValidPosition(startPosition))
            {
                var errorMessage = $"The starting position ({startPosition}) is not on the board";
                throw new InvalidOperationException(errorMessage);
            }

            if (!chessBoard.IsValidPosition(targetPosition))
            {
                var errorMessage = $"The target position ({targetPosition}) is not on the board";
                throw new InvalidOperationException(errorMessage);
            }

            if (startPosition.Equals(targetPosition))
            {
                var errorMessage = $"The starting position ({startPosition}) was the same as the target position ({targetPosition})";
                throw new InvalidOperationException(errorMessage);
            }

            var pieceInSource = chessBoard.GetPiece(startPosition);
            var pieceInTarget = chessBoard.GetPiece(targetPosition);

            if (pieceInSource == null)
            {
                var errorMessage = $"There is no piece in the starting position ({startPosition})";
                throw new InvalidOperationException(errorMessage);
            }

            var moveChecker = new MoveChecker();
            var move = new Move(startPosition, targetPosition);

            if (!moveChecker.IsValidMovement(chessBoard, pieceInSource, move))
            {
                var errorMessage = $"This move (Rows: {move.X}, Cols: {move.Y}) is not valid for this piece ({pieceInSource}).";
                throw new InvalidOperationException(errorMessage);
            }

            if (moveChecker.MoveIsBlocked(chessBoard, pieceInSource, pieceInTarget, targetPosition, move))
            {
                var errorMessage = $"This move (Rows: {move.X}, Cols: {move.Y}) is blocked by another piece.";
                throw new InvalidOperationException(errorMessage);
            }
            
            if (moveChecker.WouldLeaveInCheck())
            {
                var errorMessage = $"This move (Rows: {move.X}, Cols: {move.Y}) would leave the current player in check.";
                throw new InvalidOperationException(errorMessage);
            }

            // make move (inc removal of pieces)
            
            // return updated board
            return new ChessGame
            {
                Board = new ChessBoard().GetBoard(),
                ActivePlayer = "white",
                GameState = ""
            };
        }

        public List<Position> GetValidPositions(ChessBoard chessGame, Position position)
        {
            return new List<Position>();
        }
    }
}

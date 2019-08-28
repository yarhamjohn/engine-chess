using System;
using System.Collections.Generic;
using System.Linq;
using engine.Pieces;

namespace engine.Game
{
    public class MoveChecker
    {
        public bool IsInCheck()
        {
            throw new NotImplementedException();
        }

        public bool WouldLeaveInCheck()
        {
            throw new NotImplementedException();
        }
        
        public bool IsValidMovement(ChessBoard board, ChessPiece piece, Move move)
        {
            return IsValidNormalMove(piece, move) || IsValidSpecialMove(piece, move) && SpecialMoveIsPossible(board, piece, move);
        }

        public bool MoveIsBlocked(ChessBoard board, ChessPiece pieceInSource, ChessPiece pieceInTarget, Position targetPosition, Move move)
        {
            if (pieceInSource.Colour == pieceInTarget?.Colour)
            {
                return true;
            }
            
            if (pieceInSource.Type == "Pawn" && move.Y == 0)
            {
                return pieceInTarget != null;
            }

            if (pieceInSource.Type == "Knight")
            {
                return false;
            }

            var sourcePosition = board.GetPosition(pieceInSource);
            return sourcePosition.GetIntermediatePositions(targetPosition).Any(p => p != null);
        }

        private bool SpecialMoveIsPossible(ChessBoard board, ChessPiece piece, Move move)
        {
            switch (piece.Type)
            {
                // en passant
                case "Pawn":
                    return !piece.HasMoved;
                case "King":
                    return !piece.HasMoved && CanCastle(board, piece, move);
                default:
                    throw new ArgumentException($"This piece ({piece}) does not have any special moves.");
            }
        }

        private bool IsValidSpecialMove(ChessPiece pieceToMove, Move move)
        {
            return pieceToMove.SpecialMoves.Contains((move.X, move.Y));
        }

        private bool IsValidNormalMove(ChessPiece pieceToMove, Move move)
        {
            return pieceToMove.NormalMoves.Contains((move.X, move.Y));
        }

        private bool CanCastle(ChessBoard board, ChessPiece piece, Move move)
        {
            if (piece.Type != "King")
            {
                throw new ArgumentException($"Only Kings can attempt to castle. The culprit piece was: {piece}");
            }
            
            var kingPosition = board.GetPosition(piece);
            var targetCastlePosition = move.Y > 0
                ? new Position(kingPosition.Row, 7)
                : new Position(kingPosition.Row, 0);
            var targetPositionPiece = board.GetPiece(new Position(kingPosition.Row, targetCastlePosition.Column));

            if (targetPositionPiece == null || targetPositionPiece.Type != "Rook" || targetPositionPiece.HasMoved)
            {
                return false;
            }

            if (CastlingIsBlocked(board, kingPosition, targetCastlePosition))
            {
                return false;
            }

            var attackedPosition = new Position(kingPosition.Row, kingPosition.Column + move.Y / 2);
            if (PositionIsBeingAttacked(board, attackedPosition))
            {
                return false;
            }

            if (IsInCheck() || WouldLeaveInCheck())
            {
                return false;
            }

            return true;
        }

        private bool CastlingIsBlocked(ChessBoard board, Position kingPosition, Position castlePosition)
        {
            var colsToCheck = kingPosition.Column > castlePosition.Column ? new List<int> {1, 2, 3} : new List<int> {5, 6};
            return colsToCheck.Select(col => board.GetPiece(new Position(kingPosition.Row, col))).Any(piece => piece != null);
        }
        
        private bool PositionIsBeingAttacked(ChessBoard board, Position attackedPosition)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using engine.Board;
using engine.Pieces;

namespace engine
{
    public class ChessEngine
    {
        public ChessBoard GetNewChessBoard()
        {
            var builder = new ChessBoardBuilder();
            return builder.Build();
        }

        public ChessBoard MoveChessPiece(ChessBoard chessBoard, Position sourcePosition, Position targetPosition)
        {
            if (PositionIsOffTheBoard(sourcePosition))
            {
                chessBoard.ErrorMessage = $"The starting position ({sourcePosition}) is not on the board";
                return chessBoard;
            }

            if (PositionIsOffTheBoard(targetPosition))
            {
                chessBoard.ErrorMessage = $"The targetPosition ({targetPosition}) is not on the board";
                return chessBoard;
            }

            if (sourcePosition.Equals(targetPosition))
            {
                chessBoard.ErrorMessage =
                    $"The starting position ({sourcePosition}) was the same as the target position ({targetPosition})";
                return chessBoard;
            }

            var pieceInSource = chessBoard.ChessPieces[sourcePosition.Row, sourcePosition.Column];
            var pieceInTarget = chessBoard.ChessPieces[targetPosition.Row, targetPosition.Column];

            if (pieceInSource == null)
            {
                chessBoard.ErrorMessage = $"There is no piece in the starting position ({sourcePosition})";
                return chessBoard;
            }

            var move = new Move(sourcePosition, targetPosition);
            
            if (!IsValidNormalMove(pieceInSource, move) &&
                !IsValidSpecialMove(pieceInSource, move))
            {
                chessBoard.ErrorMessage =
                    $"This move (Rows: {move.X}, Cols: {move.Y}) is not valid for this piece ({pieceInSource}). " +
                    $"Valid normal moves are: {string.Join(", ", pieceInSource.NormalMoves)}. " +
                    $"Valid special moves are: {string.Join(", ", pieceInSource.SpecialMoves)}.";
                return chessBoard;
            }

            if (IsValidSpecialMove(pieceInSource, move) && !SpecialMoveIsPossible(chessBoard, pieceInSource, move))
            {
                chessBoard.ErrorMessage = $"This special move (Rows: {move.X}, Cols: {move.Y}) is not valid at this time.";
                return chessBoard;
            }

            var blockingPiece = GetBlockingPiece();
            if (blockingPiece != null)
            {
                var blockingPiecePosition = chessBoard.GetPiecePosition(blockingPiece);
                chessBoard.ErrorMessage = $"This move (Rows: {move.X}, Cols: {move.Y}) is blocked by another piece ({blockingPiece}) in position {blockingPiecePosition}.";
                return chessBoard;
            }

            if (IsOccupiedBySameTeam(pieceInSource, pieceInTarget))
            {
                chessBoard.ErrorMessage =
                    $"The target position ({targetPosition}) is already occupied by a piece ({pieceInTarget}) from the same team";
                return chessBoard;
            }


            // if would leave in check
            if (WouldLeaveInCheck())
            {
                chessBoard.ErrorMessage = $"This move (Rows: {move.X}, Cols: {move.Y}) would leave the current player in check.";
                return chessBoard;
            }

            // make move (inc removal of pieces)
            return new ChessBoard();
        }

        private bool SpecialMoveIsPossible(ChessBoard board, ChessPiece piece, Move move)
        {
            switch (piece.Type)
            {
                case "Pawn":
                    return !piece.HasMoved;
                case "King":
                    return !piece.HasMoved && CanCastle(board, piece, move);
                default:
                    throw new ArgumentException($"This piece ({piece}) does not have any special moves.");
            }
        }

        private bool CanCastle(ChessBoard board, ChessPiece king, Move move)
        {
            var kingRow = board.GetPiecePosition(king).Row;
            var targetCastlePosition = move.X > 0 ? board.ChessPieces[kingRow, 7] : board.ChessPieces[kingRow, 0];
            if (targetCastlePosition == null || targetCastlePosition.Type != "Castle" || targetCastlePosition.HasMoved)
            {
                return false;
            }

//            if (IsBlocked())
//            {
//                return false;
//            }
//
//            if (IsInCheck())
//            {
//                return false;
//            }

            return true;
        }

        private ChessPiece GetBlockingPiece()
        {
            throw new NotImplementedException();
        }

        private bool WouldLeaveInCheck()
        {
            throw new NotImplementedException();
        }

        private bool IsValidSpecialMove(ChessPiece pieceToMove, Move move)
        {
            return pieceToMove.SpecialMoves.Contains((move.X, move.Y));
        }

        private bool IsOccupiedBySameTeam(ChessPiece pieceInSource, ChessPiece pieceInTarget)
        {
            if (pieceInTarget == null)
            {
                return false;
            }

            return pieceInSource.Colour == pieceInTarget.Colour;
        }

        private bool PositionIsOffTheBoard(Position position)
        {
            return position.Row < 0 || position.Row > 7;
        }

        private bool IsValidNormalMove(ChessPiece pieceToMove, Move move)
        {
            return pieceToMove.NormalMoves.Contains((move.X, move.Y));
        }

        public List<Position> GetValidPositions(ChessBoard chessGame, Position position)
        {
            return new List<Position>();
        }
    }

    public class Position : IEquatable<Position>
    {
        public int Row;
        public int Column;

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Row == other.Row && Column == other.Column;
        }

        public override string ToString()
        {
            return $"Row: {Row}, Col: {Column}";
        }
    }

    public class Move
    {
        public readonly int X;
        public readonly int Y;

        public Move(Position sourcePosition, Position targetPosition)
        {
            X = targetPosition.Row - sourcePosition.Row;
            Y = targetPosition.Column - sourcePosition.Column;
        }
    }
}

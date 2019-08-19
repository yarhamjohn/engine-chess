using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
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
                chessBoard.ErrorMessage =
                    $"This special move (Rows: {move.X}, Cols: {move.Y}) is not valid at this time.";
                return chessBoard;
            }

            if (MoveIsBlocked(chessBoard, pieceInSource, pieceInTarget, move))
            {
                chessBoard.ErrorMessage = $"This move (Rows: {move.X}, Cols: {move.Y}) is blocked by another piece.";
                return chessBoard;
            }
            
            if (WouldLeaveInCheck())
            {
                chessBoard.ErrorMessage =
                    $"This move (Rows: {move.X}, Cols: {move.Y}) would leave the current player in check.";
                return chessBoard;
            }

            // make move (inc removal of pieces)
            return new ChessBoard();
        }

        private bool SpecialMoveIsPossible(ChessBoard board, ChessPiece piece, Move move)
        {
            switch (piece.Type)
            {
                // en passant?
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
            var kingPosition = board.GetPiecePosition(king);
            var targetCastlePosition = move.Y > 0
                ? new Position(kingPosition.Row, 7)
                : new Position(kingPosition.Row, 0);
            var targetPositionPiece = board.ChessPieces[kingPosition.Row, targetCastlePosition.Column];

            if (targetPositionPiece == null || targetPositionPiece.Type != "Rook" || targetPositionPiece.HasMoved)
            {
                return false;
            }

            if (CastlingIsBlocked(board, kingPosition, targetCastlePosition))
            {
                return false;
            }

//            if (IsInCheck() || WouldLeaveInCheck())
//            {
//                return false;
//            }

            return true;
        }

        private bool IsInCheck()
        {
            throw new NotImplementedException();
        }

        private bool CastlingIsBlocked(ChessBoard board, Position kingPosition, Position castlePosition)
        {
            var colsToCheck = kingPosition.Column > castlePosition.Column ? new List<int> {1, 2, 3} : new List<int> {5, 6};
            return colsToCheck.Select(col => board.ChessPieces[kingPosition.Row, col]).Any(piece => piece != null);
        }

        private bool WouldLeaveInCheck()
        {
            throw new NotImplementedException();
        }

        private bool IsValidSpecialMove(ChessPiece pieceToMove, Move move)
        {
            return pieceToMove.SpecialMoves.Contains((move.X, move.Y));
        }

        private bool MoveIsBlocked(ChessBoard board, ChessPiece pieceInSource, ChessPiece pieceInTarget, Move move)
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

            var sourcePosition = board.GetPiecePosition(pieceInSource);
            var targetPosition = board.GetPiecePosition(pieceInTarget);
            if (sourcePosition.GetPositions(targetPosition).Any(p => p != null))
            {
                return true;
            }

            return false;
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

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

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

        public List<Position> GetPositions(Position position)
        {
            if (Row == position.Row && Column == position.Column)
            {
                return new List<Position>();
            }

            if (Row == position.Row)
            {
                var positions = new List<Position>();
                var colsToTraverse = position.Column - Column;

                if (colsToTraverse < 0)
                {
                    for (var i = -1; i > colsToTraverse; i--)
                    {
                        positions.Add(new Position(Row, Column + i));
                    }
                }
                else
                {
                    for (var i = 1; i < colsToTraverse; i++)
                    {
                        positions.Add(new Position(Row, Column + i));
                    }
                }

                return positions;
            }
            
            
            if (Column == position.Column)
            {
                var positions = new List<Position>();
                var rowsToTraverse = position.Row - Row;

                if (rowsToTraverse < 0)
                {
                    for (var i = -1; i > rowsToTraverse; i--)
                    {
                        positions.Add(new Position(Row + i, Column));
                    }
                }
                else
                {
                    for (var i = 1; i < rowsToTraverse; i++)
                    {
                        positions.Add(new Position(Row + 1, Column));
                    }
                }

                return positions;
            }
            
            // row + col +
            // row - col +
            // row + col -
            // row - col -

            return new List<Position>();
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

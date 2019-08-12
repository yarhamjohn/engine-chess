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
            if (PositionIsOffTheBoard(sourcePosition) || PositionIsOffTheBoard(targetPosition))
            {
                chessBoard.ErrorMessage = $"The starting position ({sourcePosition}) and/or the targetPosition ({targetPosition}) is not on the board";
                return chessBoard;
            }
            
            var pieceInSource = chessBoard.ChessPieces[sourcePosition.Row, sourcePosition.Column];
            var pieceInTarget = chessBoard.ChessPieces[targetPosition.Row, targetPosition.Column];

            if (pieceInSource == null)
            {
                chessBoard.ErrorMessage = $"There is no piece in the starting position ({sourcePosition})";
                return chessBoard;
            }
            
            if (sourcePosition.Equals(targetPosition))
            {
                chessBoard.ErrorMessage = $"The starting position ({sourcePosition}) was the same as the target position ({targetPosition})";
                return chessBoard;
            }

            if (IsOccupiedBySameTeam(pieceInSource, pieceInTarget))
            {
                chessBoard.ErrorMessage = $"The target position ({targetPosition}) is already occupied by a piece ({pieceInTarget}) from the same team";
                return chessBoard;
            }
            
            var x = targetPosition.Column - sourcePosition.Column;
            var y = targetPosition.Row - sourcePosition.Row;
            // if not a possible move - normal move or special move
            if (!IsValidNormalMove(pieceInSource, x, y) &&
                !IsValidSpecialMove(pieceInSource, x, y))
            {
                chessBoard.ErrorMessage = $"This move (Rows: {x}, Cols: {y}) is not valid for this piece ({pieceInSource}). Valid normal moves are: {string.Join(", ", "pieceInSource.NormalMoves)}.";
                return chessBoard;
            }

            // if would leave in check

            
            // make move (inc removal of pieces)
            return new ChessBoard();
        }

        private bool IsValidSpecialMove(ChessPiece pieceInSource, int x, int y)
        {
            throw new NotImplementedException();
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

        private bool IsValidNormalMove(ChessPiece pieceToMove, int x, int y)
        {

            return pieceToMove.NormalMoves.Contains((x, y));
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
}

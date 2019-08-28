using System;
using System.Linq;
using engine.Pieces;

namespace engine.Game
{
    public class ChessBoard : IEquatable<ChessBoard>
    {
        private readonly ChessPiece[,] _chessPieces = new ChessPiece[8, 8];

        public ChessBoard()
        {
        }

        public ChessBoard(ChessPiece[,] pieces)
        {
            _chessPieces = pieces;
        }
        
        public bool Equals(ChessBoard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var bothTwoDimensional = _chessPieces.Rank == other._chessPieces.Rank;
            var equalDimensions = Enumerable.Range(0, _chessPieces.Rank).All(dimension =>
                _chessPieces.GetLength(dimension) == other._chessPieces.GetLength(dimension));
            
            return bothTwoDimensional && equalDimensions &&
                   _chessPieces.Cast<ChessPiece>().SequenceEqual(other._chessPieces.Cast<ChessPiece>());
        }
        
        public bool IsValidPosition(Position position)
        {
            return position.Row >= 0 && position.Row <= 7 && position.Column >= 0 && position.Column <= 7;
        }

        public Position GetPosition(ChessPiece piece)
        {
            if (piece == null)
            {
                return null;
            }
            
            // improve this...
            for (var row = 0; row < _chessPieces.GetLength(0); row++)
            {
                for (var col = 0; col < _chessPieces.GetLength(1); col++)
                {
                    if (_chessPieces[row, col]?.Id == piece.Id)
                    {
                        return new Position(row, col);
                    }
                }
            }

            return null;
        }

        public ChessPiece GetPiece(Position position)
        {
            return _chessPieces[position.Row, position.Column];
        }

        public void SetPosition(Position position, ChessPiece piece)
        {
            _chessPieces[position.Row, position.Column] = piece;
        }

        public ChessPiece[,] GetBoard()
        {
            return _chessPieces;
        }
    }
}

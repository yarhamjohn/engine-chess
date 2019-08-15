using System;
using System.Linq;
using engine.Pieces;

namespace engine.Board
{
    public class ChessBoard : IEquatable<ChessBoard>
    {
        public ChessPiece[,] ChessPieces = new ChessPiece[8, 8];
        public string ErrorMessage;

        public bool Equals(ChessBoard other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            var bothTwoDimensional = ChessPieces.Rank == other.ChessPieces.Rank;
            var equalDimensions = Enumerable.Range(0, ChessPieces.Rank).All(dimension =>
                ChessPieces.GetLength(dimension) == other.ChessPieces.GetLength(dimension));
            
            return bothTwoDimensional && equalDimensions &&
                   ChessPieces.Cast<ChessPiece>().SequenceEqual(other.ChessPieces.Cast<ChessPiece>());
        }

        public Position GetPiecePosition(ChessPiece piece)
        {
            
            return new Position();
        }
    }
}

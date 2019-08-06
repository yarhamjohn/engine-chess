using System;
using System.Collections.Generic;
using engine.Board;

namespace engine
{
    public class ChessEngine
    {
        public ChessBoard GetNewChessBoard()
        {
            var builder = new ChessBoardBuilder();
            return builder.Build();
        }

        public ChessBoard MoveChessPiece()
        {
            return new ChessBoard();
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
    }
}

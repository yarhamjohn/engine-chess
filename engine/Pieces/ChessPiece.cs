using System;

namespace engine.Pieces
{
    public abstract class ChessPiece : IEquatable<ChessPiece>
    {
        protected abstract string Type { get; set; }
        protected abstract string Colour { get; set; }
        protected abstract string Symbol { get; set; }

        public bool Equals(ChessPiece other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Type == other.Type && Colour == other.Colour && Symbol == other.Symbol;
        }
    }
}

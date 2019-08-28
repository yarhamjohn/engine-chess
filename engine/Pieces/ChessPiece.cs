using System;
using System.Collections.Generic;

namespace engine.Pieces
{
    public abstract class ChessPiece : IEquatable<ChessPiece>
    {
        public bool HasMoved;
        public Guid Id;

        protected internal abstract string Type { get; set; }
        protected internal abstract string Colour { get; set; }
        protected abstract string Symbol { get; set; }
        protected internal abstract List<(int x, int y)> NormalMoves { get; set; }
        protected internal abstract List<(int x, int y)> SpecialMoves { get; set; }
        
        protected ChessPiece()
        {
            Id = Guid.NewGuid();
            HasMoved = false;
        }

        public bool Equals(ChessPiece other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Type == other.Type && Colour == other.Colour && Symbol == other.Symbol;
        }

        public override string ToString()
        {
            return $"{Colour} {Type}";
        }

        public void MarkAsMoved()
        {
            HasMoved = true;
        }
    }
}

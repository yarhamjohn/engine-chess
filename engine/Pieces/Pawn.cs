using System.Collections.Generic;

namespace engine.Pieces
{
    public class Pawn : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected internal sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }

        public Pawn(string colour)
        {
            Type = "Pawn";
            Colour = colour;
            Symbol = colour == "white" ? "♙" : "♟";
            NormalMoves = colour == "white" ? new List<(int x, int y)> {(-1, 0)} : new List<(int x, int y)> {(1, 0)};
        }
    }
}

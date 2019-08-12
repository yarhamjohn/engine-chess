using System.Collections.Generic;

namespace engine.Pieces
{
    public class Knight : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected internal sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }

        public Knight(string colour)
        {
            Type = "Knight";
            Colour = colour;
            Symbol = colour == "white" ? "♘" : "♞";
            NormalMoves = new List<(int x, int y)> {(2, 1), (2, -1), (-2, -1), (-2, 1), (1, 2), (-1, 2), (1, -2), (-1, -2)};
        }
    }
}

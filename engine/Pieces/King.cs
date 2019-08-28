using System.Collections.Generic;
using engine.Game;

namespace engine.Pieces
{
    public class King : ChessPiece
    {
        protected internal sealed override string Type { get; set; }
        protected internal sealed override Player Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }
        protected internal sealed override List<(int x, int y)> SpecialMoves { get; set; }

        public King(Player colour)
        {
            Type = "King";
            Colour = colour;
            Symbol = colour == Player.White ? "♔" : "♚";
            NormalMoves = new List<(int x, int y)> {(0, 1), (0, -1), (1, 0), (-1, 0), (-1, -1), (-1, 1), (1, -1), (1, 1)};
            SpecialMoves = new List<(int x, int y)> {(0, 2), (0, -2)};
        }
    }
}

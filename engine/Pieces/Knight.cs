using System.Collections.Generic;
using engine.Game;

namespace engine.Pieces
{
    public class Knight : ChessPiece
    {
        protected internal sealed override string Type { get; set; }
        protected internal sealed override Player Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }
        protected internal sealed override List<(int x, int y)> SpecialMoves { get; set; }

        public Knight(Player colour)
        {
            Type = "Knight";
            Colour = colour;
            Symbol = colour == Player.White ? "♘" : "♞";
            NormalMoves = new List<(int x, int y)> {(2, 1), (2, -1), (-2, -1), (-2, 1), (1, 2), (-1, 2), (1, -2), (-1, -2)};
            SpecialMoves = new List<(int x, int y)>();
        }
    }
}

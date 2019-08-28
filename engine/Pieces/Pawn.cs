using System.Collections.Generic;
using engine.Game;

namespace engine.Pieces
{
    public class Pawn : ChessPiece
    {
        protected internal sealed override string Type { get; set; }
        protected internal sealed override Player Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }
        protected internal sealed override List<(int x, int y)> SpecialMoves { get; set; }

        public Pawn(Player colour)
        {
            Type = "Pawn";
            Colour = colour;
            Symbol = colour == Player.White ? "♙" : "♟";
            NormalMoves = colour == Player.White
                ? new List<(int x, int y)> {(-1, 0), (-1, -1), (-1, 1)}
                : new List<(int x, int y)> {(1, 0), (1, 1), (1, -1)};
            SpecialMoves = colour == Player.White ? new List<(int x, int y)> {(-2, 0)} : new List<(int x, int y)> {(2, 0)};
        }
    }
}

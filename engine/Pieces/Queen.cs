using System.Collections.Generic;
using engine.Game;

namespace engine.Pieces
{
    public class Queen : ChessPiece
    {
        protected internal sealed override string Type { get; set; }
        protected internal sealed override Player Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }
        protected internal sealed override List<(int x, int y)> SpecialMoves { get; set; }

        public Queen(Player colour)
        {
            Type = "Queen";
            Colour = colour;
            Symbol = colour == Player.White ? "♕" : "♛";
            NormalMoves = new List<(int x, int y)>
            {
                (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), 
                (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0), (7, 0),
                (1, 1), (2, 2), (3, 3), (4, 4), (5, 5), (6, 6), (7, 7),
                (-1, -1), (-2, -2), (-3, -3), (-4, -4), (-5, -5), (-6, -6), (-7, -7),
                (-1, 1), (-2, 2), (-3, 3), (-4, 4), (-5, 5), (-6, 6), (-7, 7),
                (1, -1), (2, -2), (3, -3), (4, -4), (5, -5), (6, -6), (7, -7)
            };
            SpecialMoves = new List<(int x, int y)>();
        }
    }
}

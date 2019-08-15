using System.Collections.Generic;

namespace engine.Pieces
{
    public class Bishop : ChessPiece
    {
        protected internal sealed override string Type { get; set; }
        protected internal sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }
        protected internal sealed override List<(int x, int y)> SpecialMoves { get; set; }

        public Bishop(string colour)
        {
            Type = "Bishop";
            Colour = colour;
            Symbol = colour == "white" ? "♗" : "♝";
            NormalMoves = new List<(int x, int y)>
            {
                (1, 1), (2, 2), (3, 3), (4, 4), (5, 5), (6, 6), (7, 7),
                (-1, -1), (-2, -2), (-3, -3), (-4, -4), (-5, -5), (-6, -6), (-7, -7),
                (-1, 1), (-2, 2), (-3, 3), (-4, 4), (-5, 5), (-6, 6), (-7, 7),
                (1, -1), (2, -2), (3, -3), (4, -4), (5, -5), (6, -6), (7, -7)
            };
            SpecialMoves = new List<(int x, int y)>();
        }
    }
}

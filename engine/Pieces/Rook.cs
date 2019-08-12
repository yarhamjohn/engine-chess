using System.Collections.Generic;

namespace engine.Pieces
{
    public class Rook : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected internal sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }
        protected internal sealed override List<(int x, int y)> NormalMoves { get; set; }

        public Rook(string colour)
        {
            Type = "Rook";
            Colour = colour;
            Symbol = colour == "white" ? "♖" : "♜";
            NormalMoves = new List<(int x, int y)>
            {
                (0, 1), (0, 2), (0, 3), (0, 4), (0, 5), (0, 6), (0, 7), 
                (1, 0), (2, 0), (3, 0), (4, 0), (5, 0), (6, 0), (7, 0)
            };
        }
    }
}

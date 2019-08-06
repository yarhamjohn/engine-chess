namespace engine.Pieces
{
    public class Pawn : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }

        public Pawn(string colour)
        {
            Type = "Pawn";
            Colour = colour;
            Symbol = colour == "white" ? "♙" : "♟";
        }
    }
}

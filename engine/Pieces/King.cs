namespace engine.Pieces
{
    public class King : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }

        public King(string colour)
        {
            Type = "King";
            Colour = colour;
            Symbol = colour == "white" ? "♔" : "♚";
        }
    }
}

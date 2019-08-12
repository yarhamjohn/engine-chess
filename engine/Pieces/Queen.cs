namespace engine.Pieces
{
    public class Queen : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }

        public Queen(string colour)
        {
            Type = "Queen";
            Colour = colour;
            Symbol = colour == "white" ? "♕" : "♛";
        }
    }
}
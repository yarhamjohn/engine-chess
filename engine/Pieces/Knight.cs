namespace engine.Pieces
{
    public class Knight : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }

        public Knight(string colour)
        {
            Type = "Knight";
            Colour = colour;
            Symbol = colour == "white" ? "♘" : "♞";
        }
    }
}

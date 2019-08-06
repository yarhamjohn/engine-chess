namespace engine.Pieces
{
    public class Rook : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }

        public Rook(string colour)
        {
            Type = "Rook";
            Colour = colour;
            Symbol = colour == "white" ? "♖" : "♜";
        }
    }
}

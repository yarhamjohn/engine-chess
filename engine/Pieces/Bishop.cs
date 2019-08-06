namespace engine.Pieces
{
    public class Bishop : ChessPiece
    {
        protected sealed override string Type { get; set; }
        protected sealed override string Colour { get; set; }
        protected sealed override string Symbol { get; set; }

        public Bishop(string colour)
        {
            Type = "Bishop";
            Colour = colour;
            Symbol = colour == "white" ? "♗" : "♝";
        }
    }
}

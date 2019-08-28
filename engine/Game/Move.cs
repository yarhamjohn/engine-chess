namespace engine.Game
{
    public class Move
    {
        public readonly int X;
        public readonly int Y;

        public Move(Position startPosition, Position targetPosition)
        {
            X = targetPosition.Row - startPosition.Row;
            Y = targetPosition.Column - startPosition.Column;
        }
    }
}
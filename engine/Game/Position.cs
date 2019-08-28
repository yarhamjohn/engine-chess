using System;
using System.Collections.Generic;

namespace engine.Game
{
    public class Position : IEquatable<Position>
    {
        public readonly int Row;
        public readonly int Column;

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Row == other.Row && Column == other.Column;
        }

        public override string ToString()
        {
            return $"Row: {Row}, Col: {Column}";
        }

        public List<Position> GetIntermediatePositions(Position position)
        {
            if (Row == position.Row && Column == position.Column)
            {
                return new List<Position>();
            }

            if (Row == position.Row)
            {
                var positions = new List<Position>();
                var colsToTraverse = position.Column - Column;

                if (colsToTraverse < 0)
                {
                    for (var i = -1; i > colsToTraverse; i--)
                    {
                        positions.Add(new Position(Row, Column + i));
                    }
                }
                else
                {
                    for (var i = 1; i < colsToTraverse; i++)
                    {
                        positions.Add(new Position(Row, Column + i));
                    }
                }

                return positions;
            }
            
            
            if (Column == position.Column)
            {
                var positions = new List<Position>();
                var rowsToTraverse = position.Row - Row;

                if (rowsToTraverse < 0)
                {
                    for (var i = -1; i > rowsToTraverse; i--)
                    {
                        positions.Add(new Position(Row + i, Column));
                    }
                }
                else
                {
                    for (var i = 1; i < rowsToTraverse; i++)
                    {
                        positions.Add(new Position(Row + 1, Column));
                    }
                }

                return positions;
            }
            
            // row + col +
            // row - col +
            // row + col -
            // row - col -

            return new List<Position>();
        }
    }
}
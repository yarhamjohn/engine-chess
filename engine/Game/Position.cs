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
            var positions = new List<Position>();
            var rowsToTraverse = position.Row - Row;
            var colsToTraverse = position.Column - Column;
            var rowDirection = rowsToTraverse == 0 ? 0 : rowsToTraverse / Math.Abs(rowsToTraverse);
            var colDirection = colsToTraverse == 0 ? 0 : colsToTraverse / Math.Abs(colsToTraverse);

            if (rowsToTraverse == 0 && colsToTraverse == 0)
            {
                return positions;
            }

            if (rowsToTraverse == 0)
            {
                for (var i = Math.Abs(colDirection); i < Math.Abs(colsToTraverse); i++)
                {
                    positions.Add(new Position(Row, Column + i * colDirection));
                }
            }
            
            if (colsToTraverse == 0)
            {
                for (var i = Math.Abs(rowDirection); i < Math.Abs(rowsToTraverse); i++)
                {
                    positions.Add(new Position(Row + i * rowDirection, Column));
                }
            }

            if (rowsToTraverse != 0 && colsToTraverse != 0)
            {
                for (var i = Math.Abs(rowDirection); i < Math.Abs(rowsToTraverse); i++)
                {
                    positions.Add(new Position(Row + i * rowDirection, Column + i * colDirection));
                }
            }
            
            return positions;
        }
    }
}
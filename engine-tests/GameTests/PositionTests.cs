using System.Collections.Generic;
using engine.Game;
using NUnit.Framework;

namespace engine_tests
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void Equals_ReturnsFalse_GivenNull()
        {
            var position = new Position(0, 0);

            var result = position.Equals(null);
            
            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_GivenItself()
        {
            var position = new Position(0, 0);

            var result = position.Equals(position);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_ReturnsFalse_GivenDifferentPosition()
        {
            var positionOne = new Position(0, 0);
            var positionTwo = new Position(7, 7);

            var result = positionOne.Equals(positionTwo);
            
            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_ReturnsTrue_GivenSamePosition()
        {
            var positionOne = new Position(0, 0);
            var positionTwo = new Position(0, 0);

            var result = positionOne.Equals(positionTwo);
            
            Assert.That(result, Is.True);
        }

        [Test]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            var position = new Position(0, 0);

            var expected = position.ToString();
            
            Assert.That(expected, Is.EqualTo("Row: 0, Col: 0"));
        }

        [Test]
        public void GetIntermediatePositions_ReturnsNoPositions_GivenSameStartAndTarget()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(4, 4);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            
            Assert.That(positions.Count, Is.EqualTo(0));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_StraightUp()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(0, 4);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(1, 4),
                new Position(2, 4),
                new Position(3, 4)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_StraightDown()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(7, 4);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(5, 4),
                new Position(6, 4)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_StraightLeft()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(4, 0);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(4, 3),
                new Position(4, 2),
                new Position(4, 1)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_StraightRight()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(4, 7);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(4, 5),
                new Position(4, 6)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_DiagonalUpLeft()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(0, 0);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(3, 3),
                new Position(2, 2),
                new Position(1, 1)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_DiagonalDownLeft()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(7, 0);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(5, 3),
                new Position(6, 2)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_DiagonalUpRight()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(0, 7);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(3, 5),
                new Position(2, 6),
                new Position(1, 7)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
        
        [Test]
        public void GetIntermediatePositions_ReturnsAllPositions_DiagonalDownRight()
        {
            var startPosition = new Position(4, 4);
            var targetPosition = new Position(7, 7);

            var positions = startPosition.GetIntermediatePositions(targetPosition);
            var expectedPositions = new List<Position>
            {
                new Position(5, 5),
                new Position(6, 6)
            };
            
            Assert.That(positions, Is.EquivalentTo(expectedPositions));
        }
    }
}
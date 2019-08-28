using engine.Pieces;
using NUnit.Framework;

namespace engine_tests.PiecesTests
{
    [TestFixture]
    public class ChessPieceTests
    {
        [Test]
        public void Equals_ReturnsFalse_GivenNull()
        {
            var pieceOne = new Pawn("black");

            var result = pieceOne.Equals(null);
            
            Assert.That(result, Is.False);
        }        
        
        [Test]
        public void Equals_ReturnsTrue_GivenItself()
        {
            var piece = new Pawn("black");

            var result = piece.Equals(piece);
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void Equals_ReturnsFalse_GivenADifferentPieceType()
        {
            var pieceOne = new Pawn("black");
            var pieceTwo = new King("black");

            var result = pieceOne.Equals(pieceTwo);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Equals_ReturnsFalse_GivenADifferentPieceColour()
        {
            var pieceOne = new Pawn("black");
            var pieceTwo = new Pawn("white");

            var result = pieceOne.Equals(pieceTwo);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Equals_ReturnsTrue_GivenSamePieceTypeAndColour()
        {
            var pieceOne = new Pawn("black");
            var pieceTwo = new Pawn("black");

            var result = pieceOne.Equals(pieceTwo);
            
            Assert.That(result, Is.True);
        }

        [Test]
        public void ToString_ReturnsCorrectStringRepresentation()
        {
            var piece = new Pawn("black");

            var expected = piece.ToString();
            
            Assert.That(expected, Is.EqualTo("black Pawn"));
        }

        [Test]
        public void MarkAsMoved_CorrectlyMarksPiece()
        {
            var piece = new Pawn("black");
            Assert.That(piece.HasMoved, Is.False);
            
            piece.MarkAsMoved();
            Assert.That(piece.HasMoved, Is.True);
        }
    }
}
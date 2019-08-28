using engine.Game;
using engine.Pieces;
using NUnit.Framework;

namespace engine_tests
{
    [TestFixture]
    public class ChessBoardTests
    {
        [TestCase(0, 0, true)]
        [TestCase(7, 0, true)]
        [TestCase(0, 7, true)]
        [TestCase(7, 7, true)]
        [TestCase(-1, 0, false)]
        [TestCase(0, -1, false)]
        [TestCase(8, 0, false)]
        [TestCase(0, 8, false)]
        public void IsValidPosition_ReturnsCorrectly(int row, int col, bool expected)
        {
            var board = new ChessBoard();
            var position = new Position(row, col);

            var validPosition = board.IsValidPosition(position);
            Assert.That(validPosition, Is.EqualTo(expected));
        }

        [Test]
        public void GetPiece_ReturnsNull_WhenPositionIsEmpty()
        {
            var board = new ChessBoard();
            var position = new Position(0, 0);

            var piece = board.GetPiece(position);
            
            Assert.That(piece, Is.Null);
        }
        
        [Test]
        public void GetPiece_ReturnsExactPiece()
        {
            var board = new ChessBoard();
            var pawn = new Pawn("black");
            var position = new Position( 0, 0);
            board.SetPosition(position, pawn);
            
            var piece = board.GetPiece(position);

            Assert.That(piece.Id, Is.EqualTo(pawn.Id));
        }

        [Test]
        public void SetPosition_CorrectlyReplacesNull()
        {
            var board = new ChessBoard();
            var pawn = new Pawn("black");
            var position = new Position(0, 0);

            board.SetPosition(position, pawn);

            var piece = board.GetPiece(position);
            Assert.That(piece.Id, Is.EqualTo(pawn.Id));
        }
        
        [Test]
        public void SetPosition_CorrectlyReplacesExistingPiece()
        {
            var board = new ChessBoard();
            var pawn = new Pawn("black");
            var king = new King("black");
            var position = new Position(0, 0);

            board.SetPosition(position, pawn);
            board.SetPosition(position, king);

            var piece = board.GetPiece(position);
            Assert.That(piece.Id, Is.EqualTo(king.Id));
        }  
        
        [Test]
        public void GetBoard_ReturnsCorrectBoard()
        {
            var board = new ChessBoard();
            var pawn = new Pawn("black");
            var king = new King("black");

            board.SetPosition(new Position(0, 0), pawn);
            board.SetPosition(new Position(7, 7), king);
            
            var expectedBoard = new ChessPiece[,]
            {
                {new Pawn("black"), null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, new King("black") }
            };
            Assert.That(board.GetBoard(), Is.EqualTo(expectedBoard));
        }

        [Test]
        public void GetPosition_ReturnsNull_GivenNull()
        {
            var board = new ChessBoard();
            board.SetPosition(new Position(0, 0), new Pawn("black"));
            
            var position = board.GetPosition(null);

            Assert.That(position, Is.Null);
        }
        
        [Test]
        public void GetPosition_ReturnsCorrectPosition_GivenPieceOnBoard()
        {
            var board = new ChessBoard();
            var pawn = new Pawn("black");
            var expectedPosition = new Position(0, 0);
            board.SetPosition(expectedPosition, pawn);
            
            var actualPosition = board.GetPosition(pawn);

            Assert.That(actualPosition, Is.EqualTo(expectedPosition));
        }       
        
        [Test]
        public void GetPosition_ReturnsNull_GivenPieceNotOnTheBoard()
        {
            var board = new ChessBoard();
            var pawn = new Pawn("black");
            var expectedPosition = new Position(0, 0);
            board.SetPosition(expectedPosition, pawn);
            
            var actualPosition = board.GetPosition(new King("black"));

            Assert.That(actualPosition, Is.Null);
        }        
        
        [Test]
        public void EqualsReturnsFalse_GivenNull()
        {
            var board = new ChessBoard();
            
            var result = board.Equals(null);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void EqualsReturnsTrue_GivenItself()
        {
            var board = new ChessBoard();
            
            var result = board.Equals(board);
            
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void EqualsReturnsFalse_GivenANonMatchingBoard()
        {
            var boardOne = new ChessBoard();
            var boardTwo = new ChessBoard();
            boardTwo.SetPosition(new Position(0, 0), new Pawn("black"));
            
            var result = boardOne.Equals(boardTwo);
            
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void EqualsReturnsTrue_GivenAMatchingBoard()
        {
            var boardOne = new ChessBoard();
            boardOne.SetPosition(new Position(0, 0), new Pawn("black"));
            var boardTwo = new ChessBoard();
            boardTwo.SetPosition(new Position(0, 0), new Pawn("black"));

            var result = boardOne.Equals(boardTwo);
            
            Assert.That(result, Is.True);
        }
    }
}
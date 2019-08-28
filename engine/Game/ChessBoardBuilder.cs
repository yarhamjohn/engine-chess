using engine.Pieces;

namespace engine.Game
{
    public class ChessBoardBuilder
    {
        private ChessBoard _chessBoard;
        public ChessBoard Build()
        {
            _chessBoard = new ChessBoard();

            AddKings(_chessBoard);
            AddQueens(_chessBoard);
            AddRooks(_chessBoard);
            AddBishops(_chessBoard);
            AddKnights(_chessBoard);
            AddPawns(_chessBoard);

            return _chessBoard;
        }

        private static void AddKings(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 4), new King("black"));
            chessBoard.SetPosition(new Position( 7, 4), new King("white"));
        }

        private static void AddQueens(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 3), new Queen("black"));
            chessBoard.SetPosition(new Position( 7, 3), new Queen("white"));
        }

        private static void AddRooks(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 0), new Rook("black"));
            chessBoard.SetPosition(new Position( 0, 7), new Rook("black"));
            chessBoard.SetPosition(new Position( 7, 0), new Rook("white"));
            chessBoard.SetPosition(new Position( 7, 7), new Rook("white"));
        }

        private static void AddBishops(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 2), new Bishop("black"));
            chessBoard.SetPosition(new Position( 0, 5), new Bishop("black"));
            chessBoard.SetPosition(new Position( 7, 2), new Bishop("white"));
            chessBoard.SetPosition(new Position( 7, 5), new Bishop("white"));
        }

        private static void AddKnights(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 1), new Knight("black"));
            chessBoard.SetPosition(new Position( 0, 6), new Knight("black"));
            chessBoard.SetPosition(new Position( 7, 1), new Knight("white"));
            chessBoard.SetPosition(new Position( 7, 6), new Knight("white"));
        }

        private static void AddPawns(ChessBoard chessBoard)
        {
            const int numCols = 8;
            for (var i = 0; i < numCols; i++)
            {
                chessBoard.SetPosition(new Position( 1, i), new Pawn("black"));
                chessBoard.SetPosition(new Position( 6, i), new Pawn("white"));
            }
        }
    }
}

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
            chessBoard.SetPosition(new Position( 0, 4), new King(Player.Black));
            chessBoard.SetPosition(new Position( 7, 4), new King(Player.White));
        }

        private static void AddQueens(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 3), new Queen(Player.Black));
            chessBoard.SetPosition(new Position( 7, 3), new Queen(Player.White));
        }

        private static void AddRooks(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 0), new Rook(Player.Black));
            chessBoard.SetPosition(new Position( 0, 7), new Rook(Player.Black));
            chessBoard.SetPosition(new Position( 7, 0), new Rook(Player.White));
            chessBoard.SetPosition(new Position( 7, 7), new Rook(Player.White));
        }

        private static void AddBishops(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 2), new Bishop(Player.Black));
            chessBoard.SetPosition(new Position( 0, 5), new Bishop(Player.Black));
            chessBoard.SetPosition(new Position( 7, 2), new Bishop(Player.White));
            chessBoard.SetPosition(new Position( 7, 5), new Bishop(Player.White));
        }

        private static void AddKnights(ChessBoard chessBoard)
        {
            chessBoard.SetPosition(new Position( 0, 1), new Knight(Player.Black));
            chessBoard.SetPosition(new Position( 0, 6), new Knight(Player.Black));
            chessBoard.SetPosition(new Position( 7, 1), new Knight(Player.White));
            chessBoard.SetPosition(new Position( 7, 6), new Knight(Player.White));
        }

        private static void AddPawns(ChessBoard chessBoard)
        {
            const int numCols = 8;
            for (var i = 0; i < numCols; i++)
            {
                chessBoard.SetPosition(new Position( 1, i), new Pawn(Player.Black));
                chessBoard.SetPosition(new Position( 6, i), new Pawn(Player.White));
            }
        }
    }
}

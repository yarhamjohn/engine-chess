using engine.Pieces;

namespace engine.Board
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
            chessBoard.ChessPieces[0, 4] = new King("black");
            chessBoard.ChessPieces[7, 4] = new King("white");
        }

        private static void AddQueens(ChessBoard chessBoard)
        {
            chessBoard.ChessPieces[0, 3] = new Queen("black");
            chessBoard.ChessPieces[7, 3] = new Queen("white");
        }

        private static void AddRooks(ChessBoard chessBoard)
        {
            chessBoard.ChessPieces[0, 0] = new Rook("black");
            chessBoard.ChessPieces[0, 7] = new Rook("black");
            chessBoard.ChessPieces[7, 0] = new Rook("white");
            chessBoard.ChessPieces[7, 7] = new Rook("white");
        }

        private static void AddBishops(ChessBoard chessBoard)
        {
            chessBoard.ChessPieces[0, 2] = new Bishop("black");
            chessBoard.ChessPieces[0, 5] = new Bishop("black");
            chessBoard.ChessPieces[7, 2] = new Bishop("white");
            chessBoard.ChessPieces[7, 5] = new Bishop("white");
        }

        private static void AddKnights(ChessBoard chessBoard)
        {
            chessBoard.ChessPieces[0, 1] = new Knight("black");
            chessBoard.ChessPieces[0, 6] = new Knight("black");
            chessBoard.ChessPieces[7, 1] = new Knight("white");
            chessBoard.ChessPieces[7, 6] = new Knight("white");
        }

        private static void AddPawns(ChessBoard chessBoard)
        {
            var arrayDimensionLength = chessBoard.ChessPieces.GetLength(0);

            for (var i = 0; i < arrayDimensionLength; i++)
            {
                chessBoard.ChessPieces[1, i] = new Pawn("black");
                chessBoard.ChessPieces[6, i] = new Pawn("white");
            }
        }
    }
}

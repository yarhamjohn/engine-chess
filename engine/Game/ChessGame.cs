using System.Collections.Generic;
using engine.Pieces;

namespace engine.Game
{
    public class ChessGame
    {
        public ChessPiece[,] Board;
        public Player ActivePlayer;
        public string GameState; // Enum?
        public List<ChessPiece> RemovedPieces;
        // Something to do with checkmate?
    }
}
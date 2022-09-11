using System.Collections.Generic;

namespace Sources.GameLogic.Core
{
    public class GameResult
    {
        public GameResult(PlayerColor winnerColor, IEnumerable<BoardIndex> winningIndices)
        {
            WinnerColor = winnerColor;
            WinningIndices = winningIndices;
        }

        public PlayerColor WinnerColor { get; }
        public IEnumerable<BoardIndex> WinningIndices { get; }
    }
}
using System;
using Sources.GameLogic.Core;
using UnityEngine;

namespace Sources.GameView
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private PeaksPlacer _placer;

        private GameState _game;

        public void UpdateGameState(GameState game)
        {
            _game = game;
        }
        
        public Vector3 GetFreeStonePosition(int row, int column)
        {
            Peak peak = GetPeak(row, column);
            int freeHeight = _game.Board.GetHighestPeak(row, column);
            return peak.GetStonePosition(freeHeight);
        }

        public Peak GetPeak(int row, int column)
        {
            foreach (var peak in _placer.Peaks)
            {
                if (peak.Row == row && peak.Column == column)
                    return peak;
            }

            throw new IndexOutOfRangeException();
        }
    }
}
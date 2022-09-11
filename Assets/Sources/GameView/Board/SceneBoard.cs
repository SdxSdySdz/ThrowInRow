using System;
using System.Collections.Generic;
using System.Linq;
using Sources.GameLogic.Core;
using UnityEngine;

namespace Sources.GameView.Board
{
    public class SceneBoard : MonoBehaviour
    {
        [SerializeField] private PeaksPlacer _placer;
        [SerializeField] private Stone _winningStonePrefab;

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

        public void ShowWinningStones(IEnumerable<BoardIndex> winningIndices)
        {
            List<Vector3> winningPositions = new List<Vector3>(winningIndices.Count());
            foreach (var index in winningIndices)
            {
                Peak peak = GetPeak(index.Row, index.Column);
                winningPositions.Add(peak.GetStonePosition(index.Peak));
            }

            IEnumerable<Stone> stones = FindObjectsOfType<Stone>();
            foreach (var stone in stones)
            {
                Destroy(stone.gameObject);
            }

            foreach (var position in winningPositions)
            {
                Instantiate(_winningStonePrefab, position, Quaternion.identity);
            }
        }
    }
}
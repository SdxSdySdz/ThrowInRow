using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.GameLogic.Core
{
    public class GameState
    {
        private readonly int _winningStoneCount = 4;
        private readonly Board _board;
        private readonly GameState _previousState;
        private readonly Move _lastMove;
        
        private PlayerColor _playerToMove;
        
        private GameState(Board board, PlayerColor playerToMove, GameState previousState, Move lastMove)
        {
            _board = board;
            _playerToMove = playerToMove;
            _previousState = previousState;
            _lastMove = lastMove;
        }
        
        public Board Board => _board;
        public PlayerColor PlayerColorToMove => _playerToMove;
        public Move LastMove => new Move(_lastMove);
        public static GameState NewGame => new GameState(new Board(), PlayerColor.White, null, Move.EmptyMove);
        
        public GameState ApplyMove(Move move)
        {
            Board nextBoard = _board.Copy();
            nextBoard.PlaceStone(_playerToMove, move);
            return new GameState(nextBoard, _playerToMove.Opposite, this, move);
        }

        public GameState ReturnMove()
        {
            if (_previousState != null)
                return _previousState;
            else
                return this;
        }

        public List<Move> GetPossibleMoves()
        {
            List<Move> moves = new List<Move>();

            for (int row = 0; row < _board.RowCount; row++)
            {
                for (int column = 0; column < _board.ColumnCount; column++)
                {
                    var move = new Move(row, column);
                    if (IsValidMove(move))
                        moves.Add(move);
                }
            }

            return moves;
        }

        public bool IsValidMove(Move move)
        {
            return _board.IsValidIndex(move.Row, move.Column) && _board.IsAvailablePeak(move.Row, move.Column);
        }

        public bool TryGetWinner(out PlayerColor winnerColor, out List<(int Row, int Column, int Peak)> winningIndices)
        {
            if (CheckWinByLastMoveDebug(out winningIndices))
            {
                winnerColor = PlayerColorToMove.Opposite;
                return true;
            }
            else
            {
                // racism
                winnerColor = PlayerColor.White;
                return false;
            }
        }
        
        private bool CheckWinByLastMoveDebug(out List<(int Row, int Column, int Peak)> winningIndices)
        {
            int requiredToWinCount = 2 * _winningStoneCount - 1;
            if (_board.StoneCount < requiredToWinCount)
            {
                winningIndices = null;
                return false;
            }

            winningIndices = new List<(int Row, int Column, int Peak)>();
            int row = _lastMove.Row;
            int column = _lastMove.Column;
            int peak = _board.GetHighestPeak(row, column);

            int rowHorizontalSum = Mathf.Abs(_board.GetHorizontalSumByRowPlane(column, peak));
            if (rowHorizontalSum == _winningStoneCount)
            {
                for (int i = 0; i < _winningStoneCount; i++)
                {
                    winningIndices.Add((i, column, peak));
                }
            
                return true;
            }

            int columnHorizontalSum = Mathf.Abs(_board.GetHorizontalSumByColumnPlane(row, peak));
            if (columnHorizontalSum == _winningStoneCount)
            {
                for (int i = 0; i < _winningStoneCount; i++)
                {
                    winningIndices.Add((row, i, peak));
                }

                return true;
            }

            int verticalSum = Mathf.Abs(_board.GetVerticalSum(row, column));
            if (verticalSum == _winningStoneCount)
            {
                for (int i = 0; i < _winningStoneCount; i++)
                {
                    winningIndices.Add((row, column, i));
                }

                return true;
            }

            List<BoardIndex[]> diagonalsIndices = _board.GetAllDiagonalsIndices();
            foreach (var indices in diagonalsIndices)
            {
                int sum = indices
                    .Select(index => _board.GetStone(index.Row, index.Column, index.Peak))
                    .Sum();
                if (Mathf.Abs(sum) == _winningStoneCount)
                {
                    winningIndices = indices
                        .Select(index => (index.Row, index.Column, index.Peak))
                        .ToList();
                    return true;
                }
            }
            /*List<int[]> diagonals = _board.GetAllDiagonals();
            foreach (var diagonal in diagonals)
            {
                if (Mathf.Abs(diagonal.Sum()) == _winningStoneCount)
                {
                    return true;
                }
            }*/

            winningIndices = null;
            return false;
        }


        private bool CheckWin(out List<(int Row, int Column, int Peak)> winningIndices)
        {
            int sum = 0;

            for (int column = 0; column < _board.ColumnCount; column++)
            {
                for (int peak = 0; column < _board.ColumnCount; column++)
                {
                    sum = _board.GetHorizontalSumByRowPlane(column, peak);
                    if (Mathf.Abs(sum) == _winningStoneCount)
                    {
                        winningIndices = null;
                        return true;
                    }
                }
            }


            for (int row = 0; row < _board.RowCount; row++)
            {
                for (int peak = 0; peak < _board.PeakCount; peak++)
                {
                    _board.GetHorizontalSumByColumnPlane(row, peak);
                    if (Mathf.Abs(sum) == _winningStoneCount)
                    {
                        winningIndices = null;
                        return true;
                    }
                }
            }


            for (int row = 0; row < _board.RowCount; row++)
            {
                for (int column = 0; column < _board.ColumnCount; column++)
                {
                    sum = _board.GetVerticalSum(row, column);
                    if (Mathf.Abs(sum) == _winningStoneCount)
                    {
                        winningIndices = null;
                        return true;
                    }
                }
            }


            List<int[]> diagonals = _board.GetAllDiagonals();
            foreach (var diagonal in diagonals)
            {
                sum = diagonal.Sum();
                if (Mathf.Abs(sum) == _winningStoneCount)
                {
                    winningIndices = null;
                    return true;
                }
            }

            winningIndices = null;
            return false;
        }
    }
}

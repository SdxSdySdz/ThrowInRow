using System;
using System.Collections.Generic;
using Sources.GameLogic.Zobrist;

namespace Sources.GameLogic.Core
{
    public class Board
    {
        private readonly int _rowCount = 4;
        private readonly int _columnCount = 4;
        private readonly int _peakCount = 4;
        private readonly int[,] _peakMap;
        private readonly int[,,] _grid;

        public ulong ZobristHash { get; private set; }
        public int RowCount => _rowCount;
        public int ColumnCount => _columnCount;
        public int PeakCount => _peakCount;
        public int Size => RowCount * ColumnCount * PeakCount;

        public int StoneCount
        {
            get
            {
                int count = 0;
                foreach (var peak in _peakMap)
                {
                    count += peak;
                }

                return count;
            }
        }

        public Board()
        {
            _peakMap = new int[_rowCount, _columnCount];
            _grid = new int[_rowCount, _columnCount, _peakCount];
            ZobristHash = ZobristHasher.EmptyBoard;
        }

        public void PlaceStone(PlayerColor playerColor, Move move)
        {
            int row = move.Row;
            int column = move.Column;
            int peak = _peakMap[row, column];
            _grid[row, column, peak] = playerColor.ColorValue;

            _peakMap[row, column] += 1;

            ZobristHash ^= ZobristHasher.GetStoneHash(row, column, peak, playerColor);
        }


        public void RemoveStone(Move move)
        {
            int row = move.Row;
            int column = move.Column;

            _peakMap[row, column] -= 1;
            int peak = _peakMap[row, column];
            int stone = _grid[row, column, peak];
            _grid[row, column, peak] = 0;

            PlayerColor playerColor = stone == 1 ? PlayerColor.White : PlayerColor.Black;
            ZobristHash ^= ZobristHasher.GetStoneHash(row, column, peak, playerColor);
        }


        public int GetStone(int row, int column, int peak)
        {
            if (IsValidIndex(row, column, peak) == false)
                throw new IndexOutOfRangeException($"Incorrect index [{row} {column} {peak}]");

            return _grid[row, column, peak];
        }

        public int GetHighestPeak(int row, int column)
        {
            return _peakMap[row, column] - 1;
        }

        public List<int[]> GetAllVerticals()
        {
            var verticals = new List<int[]>();

            for (int row = 0; row < _rowCount; row++)
            {
                for (int column = 0; column < _columnCount; column++)
                {
                    verticals.Add(GetVertical(row, column));
                }
            }

            return verticals;
        }

        public List<int[]> GetAllDiagonals()
        {
            List<int[]> diagonals = new List<int[]>();

            for (int row = 0; row < _rowCount; row++)
            {
                int[] diagonal = new int[]
                {
                    _grid[row, 0, 0],
                    _grid[row, 1, 1],
                    _grid[row, 2, 2],
                    _grid[row, 3, 3],
                };
                diagonals.Add(diagonal);

                int[] sideDiagonal = new int[]
                {
                    _grid[row, 0, 3],
                    _grid[row, 1, 2],
                    _grid[row, 2, 1],
                    _grid[row, 3, 0],
                };
                diagonals.Add(sideDiagonal);
            }

            for (int column = 0; column < _columnCount; column++)
            {
                int[] diagonal = new int[]
                {
                    _grid[0, column, 0],
                    _grid[1, column, 1],
                    _grid[2, column, 2],
                    _grid[3, column, 3],
                };
                diagonals.Add(diagonal);

                int[] sideDiagonal = new int[]
                {
                    _grid[0, column, 3],
                    _grid[1, column, 2],
                    _grid[2, column, 1],
                    _grid[3, column, 0],
                };
                diagonals.Add(sideDiagonal);
            }

            for (int peak = 0; peak < _peakCount; peak++)
            {
                int[] diagonal = new int[]
                {
                    _grid[0, 0, peak],
                    _grid[1, 1, peak],
                    _grid[2, 2, peak],
                    _grid[3, 3, peak],
                };
                diagonals.Add(diagonal);

                int[] sideDiagonal = new int[]
                {
                    _grid[0, 3, peak],
                    _grid[1, 2, peak],
                    _grid[2, 1, peak],
                    _grid[3, 0, peak],
                };
                diagonals.Add(sideDiagonal);
            }

            int[] bigAscendingDiagonal = new int[]
            {
                _grid[0, 0, 0],
                _grid[1, 1, 1],
                _grid[2, 2, 2],
                _grid[3, 3, 3],
            };
            diagonals.Add(bigAscendingDiagonal);

            int[] bigDescendingDiagonal = new int[]
            {
                _grid[0, 0, 3],
                _grid[1, 1, 2],
                _grid[2, 2, 1],
                _grid[3, 3, 0],
            };
            diagonals.Add(bigDescendingDiagonal);

            int[] bigAscendingSideDiagonal = new int[]
            {
                _grid[0, 3, 0],
                _grid[1, 2, 1],
                _grid[2, 1, 2],
                _grid[3, 0, 3],
            };
            diagonals.Add(bigAscendingSideDiagonal);

            int[] bigDescendingSideDiagonal = new int[]
            {
                _grid[0, 3, 3],
                _grid[1, 2, 2],
                _grid[2, 1, 1],
                _grid[3, 0, 0],
            };
            diagonals.Add(bigDescendingSideDiagonal);

            return diagonals;
        }

        // not optimized
        /*        public List<int[]> GetDiagonals(int row, int column, int peak)
            {
                List<int[]> diagonals = GetAllDiagonals();
                diagonals.Where(diagonal => )
            }*/

        public int[] GetVertical(int row, int column)
        {
            int[] vertical = new int[_peakCount];
            for (int peak = 0; peak < _peakCount; peak++)
            {
                vertical[peak] = _grid[row, column, peak];
            }

            return vertical;
        }

        public int GetVerticalSum(int row, int column)
        {
            int sum = 0;
            for (int peak = 0; peak < _peakCount; peak++)
            {
                sum += _grid[row, column, peak];
            }

            return sum;
        }

        public int GetHorizontalSumByRowPlane(int column, int peak)
        {
            int sum = 0;
            for (int row = 0; row < _rowCount; row++)
            {
                sum += _grid[row, column, peak];
            }

            return sum;
        }

        public int GetHorizontalSumByColumnPlane(int row, int peak)
        {
            int sum = 0;
            for (int column = 0; column < _columnCount; column++)
            {
                sum += _grid[row, column, peak];
            }

            return sum;
        }

        public Board Copy()
        {
            Board board = new Board();

            for (int row = 0; row < _rowCount; row++)
            {
                for (int column = 0; column < _columnCount; column++)
                {
                    board._peakMap[row, column] = _peakMap[row, column];

                    for (int peak = 0; peak < _peakCount; peak++)
                    {
                        board._grid[row, column, peak] = _grid[row, column, peak];
                    }
                }
            }

            board.ZobristHash = ZobristHash;

            return board;
        }

        public bool IsAvailablePeak(int row, int column)
        {
            return _peakMap[row, column] < 4;
            // return _grid[move.Row, move.Column, _peakCount - 1] == 0;
        }

        public bool IsValidIndex(int row, int column)
        {
            bool rowCondition = 0 <= row && row <= _rowCount;
            bool columnCondition = 0 <= column && column <= _columnCount;

            return rowCondition && columnCondition;
        }

        private bool IsValidIndex(int row, int column, int peak)
        {
            bool isValidRow = 0 <= row && row < _rowCount;
            bool isValidColumn = 0 <= column && column < _columnCount;
            bool isValidPeak = 0 <= peak && peak < _peakCount;

            return isValidRow && isValidColumn && isValidPeak;
        }
    }
}
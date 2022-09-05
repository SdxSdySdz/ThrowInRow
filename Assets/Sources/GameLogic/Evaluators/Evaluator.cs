using Sources.GameLogic.Core;

namespace Sources.GameLogic.Evaluators
{
    public static class Evaluator
    {
        private static readonly int[,,] WeightMap = new int[4, 4, 4]
        {
            { { 4, 1, 1, 4 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 4, 1, 1, 4 } },
            { { 1, 1, 1, 1 }, { 1, 4, 4, 1 }, { 1, 4, 4, 1 }, { 1, 1, 1, 1 } },
            { { 1, 1, 1, 1 }, { 1, 4, 4, 1 }, { 1, 4, 4, 1 }, { 1, 1, 1, 1 } },
            { { 4, 1, 1, 4 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 4, 1, 1, 4 } },
        };

        public static double MaxScore => 100;
        public static double MinScore => -100;

        public static double WeightOrientedScore(Board board)
        {
            int weightedScore = 0;
            for (int row = 0; row < board.RowCount; row++)
            {
                for (int column = 0; column < board.ColumnCount; column++)
                {
                    for (int peak = 0; peak < board.PeakCount; peak++)
                    {
                        weightedScore += board.GetStone(row, column, peak) * WeightMap[row, column, peak];
                    }
                }
            }

            return weightedScore;
        }
    }
}

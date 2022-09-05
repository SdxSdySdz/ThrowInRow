namespace Sources.GameLogic.Core
{
    public static class MoveConverter
    {
        private static readonly char[] IndexToLetter;

        static MoveConverter()
        {
            IndexToLetter = new char[] { 'A', 'B', 'C', 'D'};
        }

        public static string MoveToString(Move move)
        {
            return move == Move.EmptyMove ? "..." : $"{IndexToLetter[move.Column]}{move.Row + 1}";
        }
    }
}

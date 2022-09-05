namespace Sources.GameLogic.Core
{
    public class BoardIndex
    {
        public BoardIndex(int row, int column, int peak)
        {
            Row = row;
            Column = column;
            Peak = peak;
        }

        public int Row { get; }
        public int Column { get; }
        public int Peak { get; }
    }
}
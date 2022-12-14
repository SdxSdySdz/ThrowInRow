using UnityEngine;

namespace Sources.GameView.Board
{
    public class Peak : MonoBehaviour
    {
        [SerializeField] private Transform _bottom;
        [SerializeField] private float _stonesDistanceBetween;

        public int Row { get; private set; }

        public int Column { get; private set; }

        private void Awake()
        {
            Row = -1;
            Column = -1;
        }

        public void Init(int row, int column)
        {
            Row = row;
            Column = column;
            gameObject.name = $"Peak[{row}, {column}]";
        }

        public Vector3 GetStonePosition(int freeHeight)
        {
            return _bottom.position + Vector3.up * (_stonesDistanceBetween * freeHeight);
        }
    }
}
using Sources.GameLogic.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.StoneThrowers
{
    public abstract class StoneThrower : MonoBehaviour
    {
        [SerializeField] protected Human _human;

        public event UnityAction<Move> MoveRequested;
        
        public void ThrowStone(Vector3 position)
        {
            _human.ThrowStone(position);
        }

        protected void RequestMove(Move move)
        {
            MoveRequested?.Invoke(move);
        }

        public void Win()
        {
            _human.Win();
        }

        public void Lose()
        {
            _human.Lose();
        }
    }
}
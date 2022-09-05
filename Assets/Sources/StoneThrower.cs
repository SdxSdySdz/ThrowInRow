﻿using Sources.GameLogic.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Sources
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
    }
}
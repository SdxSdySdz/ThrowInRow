using System;
using Sources.GameLogic.AI.OpenningBooks;
using Sources.GameLogic.Core;
using UnityEngine;

namespace Sources.GameLogic.AI.Bots
{
    public abstract class Bot
    {
        protected OpeningBook _openingBook;

        public OpeningBook OpeningBook => _openingBook;
        public event Action<Move> MoveSelected;

        protected Bot()
        {
            _openingBook = new OpeningBook();
        }

        protected abstract Policy ApplyStrategy(GameState gameState);

        public Move SelectMove(GameState gameState)
        {
            (Move move, _) = ApplyStrategy(gameState).BestItem;
            MoveSelected?.Invoke(move);

            return move;
        }
    }
}

using System.Collections.Generic;
using Sources.GameLogic.Core;
using UnityEngine;

namespace Sources.GameLogic.AI.Bots
{
    public class RandomBot : Bot
    {
        protected override Policy ApplyStrategy(GameState gameState)
        {
            List<Move> possibleMoves = gameState.GetPossibleMoves();
            int randomIndex = Random.Range(0, possibleMoves.Count);
            Move randomMove = possibleMoves[randomIndex];
            return new Policy(randomMove);
        }
    }
}
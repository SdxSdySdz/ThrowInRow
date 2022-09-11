using Sources.GameLogic.Core;
using Sources.GameView.Board;
using Sources.Infrastructure.StateMachine.States.Core;
using Sources.StoneThrowers;
using Sources.StoneThrowers.Enemies;
using UnityEngine;

namespace Sources.Infrastructure.StateMachine.States
{
    public class GameResultState : PayloadedState<GameResult>
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private SceneBoard sceneBoard;

        protected override void OnEnter()
        {
            sceneBoard.ShowWinningStones(Payload.WinningIndices);

            StoneThrower winner;
            StoneThrower loser;
            
            //if (_playerColor == Payload.WinningIndices)
            // TODO
            if (PlayerColor.White == Payload.WinnerColor)
            {
                winner = _player;
                loser = _enemy;
            }
            else
            {
                winner = _enemy;
                loser = _player;
            }
            
            winner.Win();
            loser.Lose();
        }
    }
}
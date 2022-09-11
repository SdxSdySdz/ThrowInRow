using Sources.GameLogic.Core;
using Sources.GameView.Board;
using Sources.Infrastructure.StateMachine.States.Core;
using Sources.StoneThrowers;
using Sources.StoneThrowers.Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Infrastructure.StateMachine.States
{
    public class GameResultState : PayloadedState<GameResult>
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private SceneBoard sceneBoard;
        [SerializeField] private Button _restartButton;
        
        protected override void OnAwake()
        {
            HideUI();
        }
        
        protected override void OnEnter()
        {
            ShowUI();
            RunGameOverAnimations();
            sceneBoard.ShowWinningStones(Payload.WinningIndices);
            
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }


        protected override void OnExit()
        {
            HideUI();
            StopGameOverAnimations();
            DestroyWinningStones();
            
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void DestroyWinningStones()
        {
            foreach (var stone in GameObject.FindObjectsOfType<Stone>())
            {
                Destroy(stone.gameObject);
            }
        }

        private void RunGameOverAnimations()
        {
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

        private void StopGameOverAnimations()
        {
            _player.Idle();
            _enemy.Idle();
        }

        private void OnRestartButtonClicked()
        {
            StateMachine.Enter<GameProcessState>();
        }
        
        private void ShowUI()
        {
            _restartButton.gameObject.SetActive(true);
        }
        
        private void HideUI()
        {
            _restartButton.gameObject.SetActive(false);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Sources.GameLogic.Core;
using Sources.GameView.Board;
using Sources.Infrastructure.StateMachine.States.Core;
using Sources.StoneThrowers;
using Sources.StoneThrowers.Enemies;
using UnityEngine;

namespace Sources.Infrastructure.StateMachine.States
{
    public class GameProcessState : IndependentState
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private SceneBoard sceneBoard;

        private GameState _game;
        private PlayerColor _playerColor;

        protected override void OnEnter()
        {
            InitWorld();

            _player.MoveRequested += OnMoveRequestedByPlayer;
            _enemy.MoveRequested += OnMoveRequestedByEnemy;
        }

        protected override void OnExit()
        {
            _player.MoveRequested -= OnMoveRequestedByPlayer;
            _enemy.MoveRequested -= OnMoveRequestedByEnemy;
        }

        private void InitWorld()
        {
            _game = GameState.NewGame;
            _playerColor = PlayerColor.White;
        }


        private void OnMoveRequestedByPlayer(Move move)
        {
            if (_game.PlayerColorToMove == _playerColor && _game.IsValidMove(move))
            {
                ApplyMove(move, _player);
                _enemy.ForceMove(_game);
            }
        }

        private void OnMoveRequestedByEnemy(Move move)
        {
            if (_game.PlayerColorToMove != _playerColor && _game.IsValidMove(move))
            {
                ApplyMove(move, _enemy);
            }
        }

        private void ApplyMove(Move move, StoneThrower thrower)
        {
            _game = _game.ApplyMove(move);
            sceneBoard.UpdateGameState(_game);

            thrower.ThrowStone(sceneBoard.GetFreeStonePosition(move.Row, move.Column));

            if (_game.TryGetWinner(out PlayerColor winnerColor, out List<(int, int, int)> indices))
                OnGameOver(winnerColor, indices);
        }

        private void OnGameOver(PlayerColor winnerColor, List<(int Row, int Column, int Peak)> winningIndices)
        {
            var result = new GameResult(
                winnerColor, 
                winningIndices.Select(index => new BoardIndex(index.Row, index.Column, index.Peak))
                );
            
            StateMachine.Enter<GameResultState, GameResult>(result);
        }
    }
}
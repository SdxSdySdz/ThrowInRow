using Sources.AI;
using Sources.GameLogic.Core;
using UnityEngine;

namespace Sources.GameView
{
    public class GameConnect4 : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Board _board;

        private GameState _game;
        private PlayerColor _playerColor;

        private void Awake()
        {
            _game = GameState.NewGame;
            _playerColor = PlayerColor.White;
        }

        private void OnEnable()
        {
            _player.MoveRequested += OnMoveRequestedByPlayer;
            _enemy.MoveRequested += OnMoveRequestedByEnemy;
        }
        
        private void OnDisable()
        {
            _player.MoveRequested -= OnMoveRequestedByPlayer;
            _enemy.MoveRequested -= OnMoveRequestedByEnemy;
        }

        private void OnMoveRequestedByPlayer(Move move)
        {
            if (_game.PlayerColorToMove == _playerColor && _game.IsValidMove(move))
            {
                ApplyMove(move, _player);
                ForceEnemyMove();
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
            _board.UpdateGameState(_game);

            thrower.ThrowStone(_board.GetFreeStonePosition(move.Row, move.Column));

            if (_game.TryGetWinner(out PlayerColor winnerColor, out _))
                OnGameOver(winnerColor);
        }

        private void ForceEnemyMove()
        {
            _enemy.ForceMove(_game);
        }
        
        private void OnGameOver(PlayerColor winnerColor)
        {
        }
    }
}
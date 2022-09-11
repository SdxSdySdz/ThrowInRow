using Sources.GameView.Board;
using UnityEngine;

namespace Sources.StoneThrowers
{
    public class Human : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Stone _stonePrefab;

        private bool _isRunning;
        private bool _isRunningLeft;

        private void Awake()
        {
            _isRunningLeft = false;
        }

        public void Run(bool isRunningRight)
        {
            SetRunningState(true);
            SetRunningDirection(isRunningRight);
        }

        public void Stop()
        {
            SetRunningState(false);
        }

        public void ThrowStone(Vector3 position)
        {
            Stone stone = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
            stone.Init(_renderer.material.color);
            stone.TakePlace(position);
            
            _animator.SetTrigger("Throw");
        }

        public void Win()
        {
            _animator.SetTrigger("Win");
        }

        public void Lose()
        {
            _animator.SetTrigger("Lose");
        }

        private void SetRunningState(bool value)
        {
            if (_isRunning == value)
                return;

            _isRunning = value;
            _animator.SetBool("IsRunning", _isRunning);
        }

        private void SetRunningDirection(bool isRunningRight)
        {
            if (_isRunningLeft == isRunningRight)
                return;
            
            _isRunningLeft = isRunningRight;
            transform.forward = -transform.forward;
        }

        public void Idle()
        {
            _animator.Play("Idle");
        }
    }
}
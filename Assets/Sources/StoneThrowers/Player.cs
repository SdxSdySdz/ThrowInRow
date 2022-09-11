using Sources.GameLogic.Core;
using Sources.GameView;
using Sources.GameView.Board;
using Sources.Services.Input;
using UnityEngine;

namespace Sources.StoneThrowers
{
    public class Player : StoneThrower
    {
        [SerializeField] private InputService _inputService;
        
        private void OnEnable()
        {
            _inputService.ColliderTouched += OnColliderTouched;
        }

        private void OnDisable()
        {
            _inputService.ColliderTouched -= OnColliderTouched;
        }

        private void Update()
        {
            if (_inputService.IsTouching && _inputService.TouchDelta.x != 0)
                _human.Run(_inputService.TouchDelta.x >= 0);
            else
                _human.Stop();
        }

        private void OnColliderTouched(Collider collider)
        {
            if (collider.TryGetComponent(out Peak peak))
                RequestMove(new Move(peak.Row, peak.Column));
        }
    }
}

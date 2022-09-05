using Sources.Services.Input;
using UnityEngine;

namespace Sources
{
    public class InputBasedRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private InputService _inputService;

        private void FixedUpdate()
        {
            float deltaX = _inputService.TouchDelta.x;
            
            if (_inputService.IsTouching == false)
                return;

            float angle = deltaX * Time.deltaTime * _rotationSpeed;
            transform.Rotate(0, angle, 0);
        }
    }
}

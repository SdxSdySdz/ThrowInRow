using UnityEngine;
using UnityEngine.Events;

namespace Sources.Services.Input
{
    public abstract class InputService : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private Vector2 _previousTouchPosition;
        
        public Vector2 TouchDelta { get; private set; }
        public abstract bool IsTouching { get; }

        public event UnityAction<Collider> ColliderTouched; 

        private void Update()
        {
            if (IsTouchedDown())
            {
                Ray ray = _camera.ScreenPointToRay(GetTouchPosition());
                if (Physics.Raycast(ray, out RaycastHit hit))
                    ColliderTouched?.Invoke(hit.collider);
            }
        }

        private void FixedUpdate()
        {
            Vector2 touchPosition = GetTouchPosition();
            TouchDelta = touchPosition - _previousTouchPosition;
            
            _previousTouchPosition = touchPosition;
        }

        protected abstract Vector3 GetTouchPosition();
        protected abstract bool IsTouchedDown();
    }
}
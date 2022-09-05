using UnityEngine;

namespace Sources.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override bool IsTouching => UnityEngine.Input.GetMouseButton(0);

        protected override Vector3 GetTouchPosition()
        {
            return UnityEngine.Input.mousePosition;
        }

        protected override bool IsTouchedDown()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }
    }
}

using System;
using UnityEngine;

namespace Sources.Services.Input
{
    public class MobileInputService : InputService
    {
        public override bool IsTouching => throw new NotImplementedException();
        
        protected override Vector3 GetTouchPosition()
        {
            throw new NotImplementedException();
        }

        protected override bool IsTouchedDown()
        {
            throw new NotImplementedException();
        }
    }
}
using UnityEngine;

namespace Sources.Infrastructure.StateMachine.States.Core
{
    public abstract class State : MonoBehaviour
    {
        protected GameStateMachine StateMachine { get; set; }
        protected SceneLoader SceneLoader { get; set; }

        private void Awake()
        {
            enabled = false;
            OnAwake();
        }

        protected virtual void OnAwake() {  }

        protected virtual void OnEnter() {  }
        protected virtual void OnExit() {  }

        public void Exit()
        {
            enabled = false;
            StateMachine = null;
            SceneLoader = null;
            OnExit();
        }
    }
}
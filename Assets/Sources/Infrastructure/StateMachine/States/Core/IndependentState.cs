using UnityEngine;

namespace Sources.Infrastructure.StateMachine.States.Core
{
    public abstract class IndependentState : State
    {
        public void Enter(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            Debug.LogError($"Enter {this.GetType().Name}");
            
            enabled = true;
            StateMachine = stateMachine;
            SceneLoader = sceneLoader;
            OnEnter();
        }
    }
}
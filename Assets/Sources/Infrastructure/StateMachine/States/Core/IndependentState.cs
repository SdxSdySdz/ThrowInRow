namespace Sources.Infrastructure.StateMachine.States.Core
{
    public abstract class IndependentState : State
    {
        public void Enter(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            enabled = true;
            StateMachine = stateMachine;
            SceneLoader = sceneLoader;
            OnEnter();
        }
    }
}
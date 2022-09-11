namespace Sources.Infrastructure.StateMachine.States.Core
{
    public abstract class PayloadedState<TPayload> : State
    {
        protected TPayload Payload { get; private set; }
        
        public void Enter(GameStateMachine stateMachine, SceneLoader sceneLoader, TPayload payload)
        {
            enabled = true;
            StateMachine = stateMachine;
            SceneLoader = sceneLoader;
            Payload = payload;
            OnEnter();
        }
    }
}
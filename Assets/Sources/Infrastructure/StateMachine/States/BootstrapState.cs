using Sources.Infrastructure.StateMachine.States.Core;

namespace Sources.Infrastructure.StateMachine.States
{
    public class BootstrapState : IndependentState
    {
        protected override void OnEnter()
        {
            SceneLoader.Load("Main", onLoaded: OnMainSceneLoaded);
        }

        private void OnMainSceneLoaded()
        {
            SceneLoader.Load("Game", onLoaded: OnGameSceneLoaded);
        }

        private void OnGameSceneLoaded()
        {
            StateMachine.Enter<GameProcessState>();
        }
    }
}
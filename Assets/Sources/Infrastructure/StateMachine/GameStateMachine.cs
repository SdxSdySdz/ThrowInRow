using System;
using System.Collections.Generic;
using Sources.Infrastructure.StateMachine.States;
using Sources.Infrastructure.StateMachine.States.Core;
using UnityEngine;

namespace Sources.Infrastructure.StateMachine
{
    public class GameStateMachine : MonoBehaviour, ICoroutineRunner
    {
        private Dictionary<Type, State> _states;

        private State _currentState;
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _states = new Dictionary<Type, State>()
            {
                { typeof(GameProcessState), GameObject.FindObjectOfType<GameProcessState>()},
                { typeof(GameResultState), GameObject.FindObjectOfType<GameResultState>()},
            };
            
            _sceneLoader = new SceneLoader(this);
        }

        private void Start()
        {
            Enter<GameProcessState>();
        }

        public void Enter<TState>() where TState : IndependentState
        {
            IndependentState independentState = ChangeState<TState>();
            independentState.Enter(this, _sceneLoader);
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : PayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(this, _sceneLoader, payload);
        }
        
        private TState ChangeState<TState>() where TState : State
        {
            _currentState?.Exit();
      
            TState state = GetState<TState>();
            _currentState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : State => 
            _states[typeof(TState)] as TState;
    }
}
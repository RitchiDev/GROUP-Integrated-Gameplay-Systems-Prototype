using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class FSM
    {
        private Dictionary<System.Type, BaseState> stateDictionary = new Dictionary<System.Type, BaseState>();
        private BaseState currentState;

        public FSM(System.Type _startState, params BaseState[] _states)
        {
            for (int i = 0; i < _states.Length; i++)
            {
                BaseState state = _states[i];
                state.Initialize(this);
                stateDictionary.Add(state.GetType(), state);
            }

            SwitchState(_startState);
        }

        public void OnUpdate()
        {
            currentState?.OnUpdate();
        }

        public void SwitchState(System.Type _newStateType)
        {
            if (!stateDictionary.ContainsKey(_newStateType))
            {
                Debug.LogWarning("Trying to switch to a state that hasn't been included!");
                return;
            }

            currentState?.OnExit();

            currentState = stateDictionary[_newStateType];

            currentState?.OnEnter();
        }
    }
}

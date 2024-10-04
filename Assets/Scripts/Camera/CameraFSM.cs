using CameraState;
using CameraSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class CameraFSM
    {
        private Dictionary<BaseCameraState, BaseCameraState> stateDictionary = new Dictionary<BaseCameraState, BaseCameraState>();
        private BaseCameraState currentState;

        public CameraFSM(BaseCameraState _startState, params BaseCameraState[] _states)
        {
            for (int i = 0; i < _states.Length; i++)
            {
                BaseCameraState state = _states[i];
                stateDictionary.Add(state, state);
            }

            SwitchState(_startState);
        }


        public void OnUpdate(AreaCameraHandler _roomCamera)
        {
            currentState?.UpdateWithCamera(_roomCamera);
        }

        public void SwitchState(BaseCameraState _newStateType)
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

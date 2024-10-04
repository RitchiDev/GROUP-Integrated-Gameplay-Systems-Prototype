using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class BaseState : ScriptableObject
    {
        protected FSM owner;

        public void Initialize(FSM _owner)
        {
            this.owner = _owner;
        }

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void OnUpdate();
    }
}

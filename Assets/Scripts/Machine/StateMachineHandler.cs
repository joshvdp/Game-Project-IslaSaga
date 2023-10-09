using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Base
{
    public abstract class StateMachineHandler<TData, TFunctions> : MonoBehaviour
    {
        [SerializeField] protected TData initialState;

        public TData InitialState => initialState;

        private TFunctions _currentState;
        public virtual TFunctions CurrentState
        {
            get => _currentState;
            protected set
            {
                _currentState = value;
            }
        }

        public virtual void Awake() => SetState(InitialState);

        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void SetState(TData newState);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using StateMachine.Base;
using System;

namespace StateMachine.Enemy.State
{
    public class FieryMachineData : StateMachineData<FieryMonoStateMachine, FieryMachineFunctions>
    {
        [SerializeField, Foldout("Animations")] protected string AnimationTrigger;
        [SerializeField, Foldout("Settings")] protected bool isUnlocked = true;
        [SerializeField, Foldout("Settings")] protected bool isDamageable = true;
        [SerializeField, Foldout("Settings")] protected bool isKnockbackable = true;
        [SerializeField, Foldout("Settings")] protected Color materialColor;

        [SerializeField] public List<FieryChangeState> statesToChangeTo;
        public bool IsUnlocked => isUnlocked;
        public bool IsDamageable => isDamageable;
        public bool IsKnockbackable => isKnockbackable;
        public Color MaterialColor => materialColor;
        public string AnimTrigger => AnimationTrigger;
        public override FieryMachineFunctions Initialize(FieryMonoStateMachine machine)
        {
            return null;
        }

       
    }
    public class FieryMachineFunctions : StateMachineFunction<FieryMonoStateMachine, FieryMachineData>
    {
        string AnimTrigger;
        protected List<FieryChangeState> _changeStates;
        public FieryMachineFunctions(FieryMonoStateMachine machine, FieryMachineData data) : base(machine, data)
        {
            AnimTrigger = data.AnimTrigger;
            machine.HpComponent.IsDamageable = data.IsDamageable;

            for (int i = 0; i < machine.MainRenderers.Length; i++)
            {
                machine.MainRenderers[i].material.color = data.MaterialColor;
            }

            if (!string.IsNullOrEmpty(AnimTrigger)) machine.Animator.SetTrigger(AnimTrigger);
            StartStateListeners(machine, data);
        }

        private void StartStateListeners(FieryMonoStateMachine machine, FieryMachineData dataHolder)
        {
            _changeStates = new List<FieryChangeState>();
            foreach (var changeState in dataHolder.statesToChangeTo)
                _changeStates.Add(new FieryChangeState(changeState, machine));
        }

        public override void Discard()
        {
            if (!string.IsNullOrEmpty(AnimTrigger))
                machine.Animator.ResetTrigger(AnimTrigger);
            foreach (var state in _changeStates)
                state.RemoveListeners();
        }

        public override void StateUpdate()
        {

        }
        public override void StateFixedUpdate()
        {

        }
    }

    [Serializable]
    public class FieryChangeState
    {
        public string Name => state.name;
        public FieryMachineData state;
        public List<FieryChangeEventsToListen> eventsToListen;
        private FieryMonoStateMachine machine;

        private bool isDoneWithStart = false;
        private List<Coroutine> routines = new List<Coroutine>();

        public FieryChangeState (FieryChangeState reference, FieryMonoStateMachine machine)
        {
            state = reference.state;
            eventsToListen = reference.eventsToListen;
            this.machine = machine;
            AddListeners();
        }
        public void AddListeners()
        {
            if (CheckIfEventsToListenIsEmpty())
                return;

            foreach (FieryChangeEventsToListen typeOfEvent in eventsToListen)
                AddListener(typeOfEvent);
        }
        private void AddListener(FieryChangeEventsToListen typeOfEvent)
        {
            isDoneWithStart = false;

            bool PlayerWithinRange() => machine.PlayerWithinDetectRange;
            bool PlayerWithinAttackRange() => machine.PlayerWithinAttackRange;


            switch (typeOfEvent)
            {
                case FieryChangeEventsToListen.PLAYER_WITHIN_RANGE:
                    routines.Add(machine.StartCoroutine(CheckFor(PlayerWithinRange)));
                    break;
                case FieryChangeEventsToListen.PLAYER_WITHIN_ATTACK_RANGE:
                    routines.Add(machine.StartCoroutine(CheckFor(PlayerWithinAttackRange)));
                    break;
                case FieryChangeEventsToListen.ON_ANIMATION_END:
                    machine.AnimationEvents.FindEvent("On Animation End").AddListener(SetState);
                    break;
                case FieryChangeEventsToListen.ON_END_STATE_CALLED:
                    machine.OnEndState += SetState;
                    break;
                case FieryChangeEventsToListen.ON_HIT:
                    machine.HpComponent.onHit.AddListener(SetState);
                    break;

            }

            isDoneWithStart = true;
        }

        public void RemoveListeners()
        {
            if (CheckIfEventsToListenIsEmpty())
                return;
            foreach (var routine in routines)
                machine.StopCoroutine(routine);

            foreach (FieryChangeEventsToListen typeOfEvent in eventsToListen)
                RemoveListener(typeOfEvent);
        }
        private void RemoveListener(FieryChangeEventsToListen typeOfEvent)
        {
            switch (typeOfEvent)
            {
                case FieryChangeEventsToListen.ON_ANIMATION_END:
                    machine.AnimationEvents.FindEvent("On Animation End").RemoveListener(SetState);
                    break;
                case FieryChangeEventsToListen.ON_END_STATE_CALLED:
                    machine.OnEndState -= SetState;
                    break;
                case FieryChangeEventsToListen.ON_HIT:
                    machine.HpComponent.onHit.RemoveListener(SetState);
                    break;
            }
        }
        private bool CheckIfEventsToListenIsEmpty()
        {
            if (eventsToListen.Count <= 0)
            {
                Debug.Log("There is no registered events to listen.");
                return true;
            }

            return false;
        }

        private void SetState()
        {
            machine.SetState(state);
        }

        private IEnumerator CheckFor(Func<bool> action)
        {
            while (!isDoneWithStart)
                yield return null;

            while (!action.Invoke())
                yield return 0.1f;

            SetState();
        }
    }

    public enum FieryChangeEventsToListen
    {
        PLAYER_WITHIN_RANGE,
        PLAYER_WITHIN_ATTACK_RANGE,
        ON_ANIMATION_END,
        ON_END_STATE_CALLED,
        ON_HIT
    }
}

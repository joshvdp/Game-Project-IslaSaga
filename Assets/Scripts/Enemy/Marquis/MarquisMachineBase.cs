using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
using NaughtyAttributes;
using System;
namespace StateMachine.Enemy
{
    public class MarquisMachineData : StateMachineData<MarquisMonoStateMachine, MarquisMachineFunctions>
    {
        [SerializeField, Foldout("Animations")] protected string AnimationTrigger;
        [SerializeField, Foldout("Settings")] protected bool isUnlocked = true;
        [SerializeField, Foldout("Settings")] protected bool isDamageable = true;
        [SerializeField, Foldout("Settings")] protected bool isKnockbackable = true;
        [SerializeField, Foldout("Settings")] protected Color materialColor;

        [SerializeField] public List<MarquisChangeState> statesToChangeTo;
        public bool IsUnlocked => isUnlocked;
        public bool IsDamageable => isDamageable;
        public bool IsKnockbackable => isKnockbackable;
        public Color MaterialColor => materialColor;
        public string AnimTrigger => AnimationTrigger;

        public override MarquisMachineFunctions Initialize(MarquisMonoStateMachine machine)
        {
            return null;
        }
    }

    public class MarquisMachineFunctions : StateMachineFunction<MarquisMonoStateMachine, MarquisMachineData>
    {
        string AnimTrigger;
        protected List<MarquisChangeState> _changeStates;
        public MarquisMachineFunctions(MarquisMonoStateMachine machine, MarquisMachineData data) : base(machine, data)
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

        private void StartStateListeners(MarquisMonoStateMachine machine, MarquisMachineData dataHolder)
        {
            _changeStates = new List<MarquisChangeState>();
            foreach (var changeState in dataHolder.statesToChangeTo)
                _changeStates.Add(new MarquisChangeState(changeState, machine));
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
    public class MarquisChangeState
    {
        public string Name => state.name;
        public MarquisMachineData state;
        public List<MarquisChangeEventsToListen> eventsToListen;
        private MarquisMonoStateMachine machine;

        private bool isDoneWithStart = false;
        private List<Coroutine> routines = new List<Coroutine>();
        public string reasonForSetState;
        public MarquisChangeState(MarquisChangeState reference, MarquisMonoStateMachine machine)
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
            foreach (MarquisChangeEventsToListen typeOfEvent in eventsToListen) AddListener(typeOfEvent);
        }
        private void AddListener(MarquisChangeEventsToListen typeOfEvent)
        {
            isDoneWithStart = false;
            bool IsPlayerWithinDetectRange() => machine.PlayerWithinDetectRange;
            bool IsPlayerWithinAttackRange() => machine.PlayerWithinAttackRange;
            switch (typeOfEvent)
            {
                case MarquisChangeEventsToListen.PLAYER_WITHIN_DETECT_RANGE:
                    routines.Add(machine.StartCoroutine(CheckFor(IsPlayerWithinDetectRange)));
                    break;
                case MarquisChangeEventsToListen.PLAYER_WITHIN_ATTACK_RANGE:
                    routines.Add(machine.StartCoroutine(CheckFor(IsPlayerWithinAttackRange)));
                    break;
                case MarquisChangeEventsToListen.ON_ANIMATION_END:
                    machine.AnimationEvents.FindEvent("On Animation End").AddListener(SetState);
                    break;
                case MarquisChangeEventsToListen.ON_ATTACK_LIGHT:
                    machine.OnStartLightAttack += SetState;
                    break;
                case MarquisChangeEventsToListen.ON_ATTACK_HEAVY:
                    machine.OnStartHeavyAttack += SetState;
                    break;
            }
            isDoneWithStart = true;
        }
        private void RemoveListener(MarquisChangeEventsToListen typeOfEvent)
        {
            switch (typeOfEvent)
            {
                case MarquisChangeEventsToListen.ON_ANIMATION_END:
                    machine.AnimationEvents.FindEvent("On Animation End").RemoveListener(SetState);
                    break;
                case MarquisChangeEventsToListen.ON_ATTACK_LIGHT:
                    machine.OnStartLightAttack -= SetState;
                    break;
                case MarquisChangeEventsToListen.ON_ATTACK_HEAVY:
                    machine.OnStartHeavyAttack -= SetState;
                    break;
            }
        }
        public void RemoveListeners()
        {
            if (CheckIfEventsToListenIsEmpty()) return;
            foreach (var routine in routines) machine.StopCoroutine(routine);
            foreach (MarquisChangeEventsToListen typeOfEvent in eventsToListen) RemoveListener(typeOfEvent);
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

        private void SetState() => machine.SetState(state);

        private IEnumerator CheckFor(Func<bool> action)
        {
            while (!isDoneWithStart)
                yield return null;

            while (!action.Invoke())
                yield return 0.1f;
            SetState();
        }
    }
    public enum MarquisChangeEventsToListen
    {
        PLAYER_WITHIN_DETECT_RANGE,
        PLAYER_WITHIN_ATTACK_RANGE,
        ON_ANIMATION_END,
        ON_ATTACK_LIGHT,
        ON_ATTACK_HEAVY
    }
}


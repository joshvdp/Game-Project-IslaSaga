using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using StateMachine.Base;
using System;

namespace StateMachine.Player
{
    public class PlayerMachineData : StateMachineData<PlayerMonoStateMachine, PlayerMachineFunctions>
    {
        [SerializeField, Foldout("Animations")] protected string AnimationTrigger;
        [SerializeField, Foldout("Animations")] protected float animationSpeed = 1f;
        [SerializeField, Foldout("Settings")] protected bool isUnlocked = true;
        [SerializeField, Foldout("Settings")] protected bool isDamageable = true;
        [SerializeField, Foldout("Settings")] protected bool isKnockbackable = true;
        [SerializeField, Foldout("Settings")] protected Color materialColor = Color.white;

        [SerializeField] public List<PlayerChangeState> statesToChangeTo;
        public bool IsUnlocked => isUnlocked;
        public bool IsDamageable => isDamageable;
        public bool IsKnockbackable => isKnockbackable;
        public Color MaterialColor => materialColor;
        public string AnimTrigger => AnimationTrigger;
        public float AnimationSpeed => animationSpeed;
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return null;
        }


    }
    public class PlayerMachineFunctions : StateMachineFunction<PlayerMonoStateMachine, PlayerMachineData>
    {
        string AnimTrigger;
        protected List<PlayerChangeState> _changeStates;
        public PlayerMachineFunctions(PlayerMonoStateMachine machine, PlayerMachineData data) : base(machine, data)
        {
            AnimTrigger = data.AnimTrigger;
            machine.HpComponent.IsDamageable = data.IsDamageable;

            for (int i = 0; i < machine.MainRenderers.Length; i++)
            {
                machine.MainRenderers[i].material.color = data.MaterialColor;
            }

            machine.Animator.speed = data.AnimationSpeed;
            if (!string.IsNullOrEmpty(AnimTrigger)) machine.Animator.SetTrigger(AnimTrigger);
            StartStateListeners(machine, data);
        }

        private void StartStateListeners(PlayerMonoStateMachine machine, PlayerMachineData dataHolder)
        {
            _changeStates = new List<PlayerChangeState>();
            foreach (var changeState in dataHolder.statesToChangeTo)
                _changeStates.Add(new PlayerChangeState(changeState, machine));
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
    public class PlayerChangeState
    {
        public string Name => state.name;
        public PlayerMachineData state;
        public List<PlayerChangeEventsToListen> eventsToListen;
        private PlayerMonoStateMachine machine;

        private bool isDoneWithStart = false;
        private List<Coroutine> routines = new List<Coroutine>();

        public PlayerChangeState(PlayerChangeState reference, PlayerMonoStateMachine machine)
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

            foreach (PlayerChangeEventsToListen typeOfEvent in eventsToListen)
                AddListener(typeOfEvent);
        }
        private void AddListener(PlayerChangeEventsToListen typeOfEvent)
        {
            isDoneWithStart = false;


            switch (typeOfEvent)
            {
                case PlayerChangeEventsToListen.ON_MOVE_INPUT:
                    machine.PlayerInputs.OnMoveInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_MOVE_INPUT:
                    machine.OnNoMoveInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_SPRINT_INPUT:
                    machine.PlayerInputs.OnSprintInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_SPRINT_INPUT:
                    machine.PlayerInputs.OnNoSprintInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ATTACK_ONE:
                    machine.PlayerInputs.AttackOne += SetState;
                    break;
                case PlayerChangeEventsToListen.ATTACK_TWO:
                    machine.PlayerInputs.AttackTwo += SetState;
                    break;
                case PlayerChangeEventsToListen.ATTACK_THREE:
                    machine.PlayerInputs.AttackThree += SetState;
                    break;
                case PlayerChangeEventsToListen.SPIN_ATTACK:
                    machine.PlayerInputs.SpinAttack += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_ANIMATION_END:
                    machine.AnimationEvents.FindEvent("On Animation End").AddListener(SetState);
                    break;
                case PlayerChangeEventsToListen.ON_PICKUP_ITEM:
                    machine.OnPickupItem += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_PICKUP_ITEM:
                    machine.OnNoItemPickup += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_PICKUP_INPUT:
                    machine.PlayerInputs.OnPickupInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_ENDSTATE_CALLED:
                    machine.OnEndstate += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_SHIELD_INPUT:
                    machine.PlayerInputs.OnShieldInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_SHIELD_INPUT:
                    machine.PlayerInputs.OnNoShieldInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_JUMP_INPUT:
                    machine.PlayerInputs.OnJumpInput += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_FALLING:
                    machine.OnFalling += SetState;
                    break;
                case PlayerChangeEventsToListen.ON_LANDED:
                    machine.OnLanded += SetState;
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

            foreach (PlayerChangeEventsToListen typeOfEvent in eventsToListen)
                RemoveListener(typeOfEvent);
        }
        private void RemoveListener(PlayerChangeEventsToListen typeOfEvent)
        {
            switch (typeOfEvent)
            {
                case PlayerChangeEventsToListen.ON_MOVE_INPUT:
                    machine.PlayerInputs.OnMoveInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_MOVE_INPUT:
                    machine.OnNoMoveInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_SPRINT_INPUT:
                    machine.PlayerInputs.OnSprintInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_SPRINT_INPUT:
                    machine.PlayerInputs.OnNoSprintInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ATTACK_ONE:
                    machine.PlayerInputs.AttackOne -= SetState;
                    break;
                case PlayerChangeEventsToListen.ATTACK_TWO:
                    machine.PlayerInputs.AttackTwo -= SetState;
                    break;
                case PlayerChangeEventsToListen.ATTACK_THREE:
                    machine.PlayerInputs.AttackThree -= SetState;
                    break;
                case PlayerChangeEventsToListen.SPIN_ATTACK:
                    machine.PlayerInputs.SpinAttack -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_ANIMATION_END:
                    machine.AnimationEvents.FindEvent("On Animation End").RemoveListener(SetState);
                    break;
                case PlayerChangeEventsToListen.ON_PICKUP_ITEM:
                    machine.OnPickupItem -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_PICKUP_ITEM:
                    machine.OnNoItemPickup -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_PICKUP_INPUT:
                    machine.PlayerInputs.OnPickupInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_ENDSTATE_CALLED:
                    machine.OnEndstate -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_SHIELD_INPUT:
                    machine.PlayerInputs.OnShieldInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_NO_SHIELD_INPUT:
                    machine.PlayerInputs.OnNoShieldInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_JUMP_INPUT:
                    machine.PlayerInputs.OnJumpInput -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_FALLING:
                    machine.OnFalling -= SetState;
                    break;
                case PlayerChangeEventsToListen.ON_LANDED:
                    machine.OnLanded -= SetState;
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

    public enum PlayerChangeEventsToListen
    {
        ON_MOVE_INPUT,
        ON_NO_MOVE_INPUT,
        ON_SPRINT_INPUT,
        ON_NO_SPRINT_INPUT,
        ATTACK_ONE,
        ATTACK_TWO,
        ATTACK_THREE,
        SPIN_ATTACK,
        ON_ANIMATION_END,
        ON_PICKUP_ITEM,
        ON_NO_PICKUP_ITEM,
        ON_PICKUP_INPUT,
        ON_ENDSTATE_CALLED,
        ON_SHIELD_INPUT,
        ON_NO_SHIELD_INPUT,
        ON_JUMP_INPUT,
        ON_FALLING,
        ON_LANDED
    }
}

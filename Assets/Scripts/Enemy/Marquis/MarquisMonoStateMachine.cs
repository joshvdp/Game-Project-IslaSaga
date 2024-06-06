using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
using System;
using Core;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace StateMachine.Enemy
{
    public class MarquisMonoStateMachine : StateMachineHandler<MarquisMachineData, MarquisMachineFunctions>
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private DetectCollider _detectCollider;
        private DetectCollider _attackRange;
        private AnimationEvents _animationEvents;
        private HpBar _hpComponent;
        private AttackCollider _attackCol;
        private EnemySpawnPosition _enemySpawnPosition;
        
        public NavMeshAgent Agent => _agent ? _agent : _agent = GetComponent<NavMeshAgent>();
        public EnemySpawnPosition EnemySpawnPosition => _enemySpawnPosition ? _enemySpawnPosition : _enemySpawnPosition = GetComponent<EnemySpawnPosition>();
        public Animator Animator => _animator ? _animator : _animator = GetComponentInChildren<Animator>();
        public DetectCollider DetectCollider => _detectCollider ? _detectCollider : _detectCollider = GetComponentInChildren<DetectCollider>();
        public DetectCollider AttackRange => _attackRange ? _attackRange : _attackRange = transform.Find("Attack Range").GetComponent<DetectCollider>();
        public AnimationEvents AnimationEvents => _animationEvents ? _animationEvents : _animationEvents = GetComponentInChildren<AnimationEvents>();
        public AttackCollider AttackCol => _attackCol ? _attackCol : _attackCol = transform.Find("Attack Collider").GetComponent<AttackCollider>();
        public HpBar HpComponent => _hpComponent ? _hpComponent : _hpComponent = GetComponent<HpBar>();
        public bool PlayerWithinDetectRange => DetectCollider.ObjectWithinDetectRange;
        public bool PlayerWithinAttackRange => AttackRange.ObjectWithinDetectRange;

        public Transform CurrentTarget;
        public Renderer[] MainRenderers;


        public Action OnEndState;
        public Action OnStartLightAttack;
        public Action OnStartHeavyAttack;

        public Action OnStun;
        public Action OnStunEnd;
        public float StunDuration = 3f;

        [SerializeField] int StunHitThreshold = 3;
        int GotHitCounter = 0;
        public override void Awake()
        {
            base.Awake();
        }
        public override void Update()
        {
            CurrentState.StateUpdate();
        }
        public override void FixedUpdate()
        {
            CurrentState.StateFixedUpdate();
        }

        public override void SetState(MarquisMachineData newState)
        {
            if (newState == null || !newState.IsUnlocked)
                return;
            CurrentState?.Discard();
            CurrentState = newState.Initialize(this);
            // Debug.Log("MARQ NEW STATE IS " + CurrentState.Data.name);
        }
        void ChooseAttack()
        {
            int randomInt = Random.Range(1, 11);
            if (randomInt <= 5) OnStartLightAttack?.Invoke();
            else OnStartHeavyAttack?.Invoke();
        }
        public void LookAtTarget() => transform.rotation =
        Quaternion.LookRotation(CurrentTarget.position - transform.position);
        public void DestroyGameobject() => Destroy(gameObject);

        public void CheckStunCondition()
        {
            GotHitCounter++;
            if(GotHitCounter >= StunHitThreshold)
            {
                ApplyStun();
            }

        }

        public void ApplyStun()
        {

            OnStun?.Invoke();
            GotHitCounter = 0;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Core;
using StateMachine.Base;
using StateMachine.Enemy.State;
using System;
using UnityEngine.Events;

public class BoximonMonoStateMachine : StateMachineHandler<BoximonMachineData, BoximonMachineFunctions>
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private DetectCollider _detectCollider;
    private AttackCollider _attackCollider;
    private AnimationEvents _animationEvents;
    private HpBar _hpComponent;
    private EnemySpawnPosition _enemySpawnPosition;
    
    public NavMeshAgent Agent => _agent ?
        _agent : _agent = GetComponent<NavMeshAgent>();
    public EnemySpawnPosition EnemySpawnPosition => _enemySpawnPosition ? 
        _enemySpawnPosition : _enemySpawnPosition = GetComponent<EnemySpawnPosition>();
    public Animator Animator => _animator ?
        _animator : _animator = GetComponentInChildren<Animator>();
    public DetectCollider DetectCollider => _detectCollider ? 
        _detectCollider : _detectCollider = GetComponentInChildren<DetectCollider>();
    public AttackCollider AttackCollider => _attackCollider ?
        _attackCollider : _attackCollider = GetComponentInChildren<AttackCollider>();
    public AnimationEvents AnimationEvents => _animationEvents ?
        _animationEvents : _animationEvents = GetComponentInChildren<AnimationEvents>();
    public HpBar HpComponent => _hpComponent ?
        _hpComponent : _hpComponent = GetComponent<HpBar>();
    public bool PlayerWithinDetectRange => DetectCollider.ObjectWithinDetectRange;
    public bool PlayerWithinAttackRange => AttackCollider.ObjectWithinAttackRange;

    public Transform CurrentTarget;
    public Renderer[] MainRenderers;

    public Action OnEndState;

    public override void Awake()
    {
        base.Awake();
        transform.parent = GameObject.FindWithTag("Enemy Container").transform;
    }
    private void Start()
    {

        HpComponent.onDeath.AddListener(InvokeAnyEnemyDeath);
    }
    public override void Update()
    {
        CurrentState.StateUpdate();
    }
    public override void FixedUpdate()
    {
        CurrentState.StateFixedUpdate();
    }
    public override void SetState(BoximonMachineData newState)
    {
        if (newState == null || !newState.IsUnlocked)
            return;
        
        CurrentState?.Discard();
        CurrentState = newState.Initialize(this);
    }
    public void LookAtTarget() => transform.rotation = 
        Quaternion.LookRotation(CurrentTarget.position - transform.position);
    public void DestroyGameobject() => Destroy(gameObject);

    #region GLOBAL EVENTS INVOKES
    void InvokeAnyEnemyDeath() => GlobalEvents.Instance.FindEvent("On Any Enemy Death").Invoke();
    #endregion

}


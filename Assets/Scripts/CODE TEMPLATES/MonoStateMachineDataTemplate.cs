using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
using System;
using StateMachine;
using NaughtyAttributes;
public class MonoStateMachineDataTemplate : StateMachineData<MonoStateMachineTemplate, MonoStateMachineFunctionTemplate>
{
    [SerializeField, Foldout("Animations")] protected string AnimationTrigger;
    [SerializeField, Foldout("Animations")] protected float animationSpeed = 1f;
    [SerializeField, Foldout("Settings")] public bool isUnlocked = true;
    [SerializeField, Foldout("Settings")] protected bool isDamageable = true;
    [SerializeField, Foldout("Settings")] protected bool isKnockbackable = true;
    [SerializeField, Foldout("Settings")] protected Color materialColor = Color.white;

    [SerializeField] public List<ChangeState> statesToChangeTo;
    public bool IsUnlocked => isUnlocked;
    public bool IsDamageable => isDamageable;
    public bool IsKnockbackable => isKnockbackable;
    public Color MaterialColor => materialColor;
    public string AnimTrigger => AnimationTrigger;
    public float AnimationSpeed => animationSpeed;
    public override MonoStateMachineFunctionTemplate Initialize(MonoStateMachineTemplate machine)
    {
        return null;
    }
}

public class MonoStateMachineFunctionTemplate : StateMachineFunction<MonoStateMachineTemplate, MonoStateMachineDataTemplate>
{
    public MonoStateMachineFunctionTemplate(MonoStateMachineTemplate machine, MonoStateMachineDataTemplate data) : base(machine, data)
    {

    }
    public override void Discard()
    {
        
    }

    public override void StateFixedUpdate()
    {
        
    }

    public override void StateUpdate()
    {
        
    }
}

[Serializable]
public class ChangeState
{
    public string Name => state.name;
    public MonoStateMachineDataTemplate state;
    public List<ChangeEventsToListen> eventsToListen;
    private MonoStateMachineTemplate machine;

    private bool isDoneWithStart = false;
    private List<Coroutine> routines = new List<Coroutine>();
    public string reasonForSetState;
    public ChangeState(ChangeState reference, MonoStateMachineTemplate machine)
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

        foreach (ChangeEventsToListen typeOfEvent in eventsToListen) AddListener(typeOfEvent);
    }
    private void AddListener(ChangeEventsToListen typeOfEvent)
    {
        isDoneWithStart = false;

        switch (typeOfEvent)
        {

        }
        isDoneWithStart = true;
    }
    public void RemoveListeners()
    {
        if (CheckIfEventsToListenIsEmpty()) return;
        foreach (var routine in routines) machine.StopCoroutine(routine);
        foreach (ChangeEventsToListen typeOfEvent in eventsToListen) RemoveListener(typeOfEvent);
    }
    private void RemoveListener(ChangeEventsToListen typeOfEvent)
    {
        switch (typeOfEvent)
        {

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
public enum ChangeEventsToListen
{

}
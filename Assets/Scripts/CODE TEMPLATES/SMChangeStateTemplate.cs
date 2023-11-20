using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
//[Serializable]
//public class ChangeState
//{
//    public string Name => state.name;
//    public StateMachineData state;
//    public List<ChangeEventsToListen> eventsToListen;
//    private StateMachineHandler machine;

//    private bool isDoneWithStart = false;
//    private List<Coroutine> routines = new List<Coroutine>();
//    public string reasonForSetState;
//    public ChangeState(PlayerChangeState reference, PlayerMonoStateMachine machine)
//    {
//        state = reference.state;
//        eventsToListen = reference.eventsToListen;
//        this.machine = machine;
//        AddListeners();
//    }
//    public void AddListeners()
//    {
//        if (CheckIfEventsToListenIsEmpty())
//            return;

//        foreach (ChangeEventsToListen typeOfEvent in eventsToListen) AddListener(typeOfEvent);
//    }
//    private void AddListener(ChangeEventsToListen typeOfEvent)
//    {
//        isDoneWithStart = false;

//        switch (typeOfEvent)
//        {
            
//        }
//        isDoneWithStart = true;
//    }

//    public void RemoveListeners()
//    {
//        if (CheckIfEventsToListenIsEmpty()) return;
//        foreach (var routine in routines) machine.StopCoroutine(routine);
//        foreach (ChangeEventsToListen typeOfEvent in eventsToListen) RemoveListener(typeOfEvent);
//    }
//    private void RemoveListener(ChangeEventsToListen typeOfEvent)
//    {
//        switch (typeOfEvent)
//        {
            
//        }

//    }
//    private bool CheckIfEventsToListenIsEmpty()
//    {
//        if (eventsToListen.Count <= 0)
//        {
//            Debug.Log("There is no registered events to listen.");
//            return true;
//        }

//        return false;
//    }

//    private void SetState() => machine.SetState(state);

//    private IEnumerator CheckFor(Func<bool> action)
//    {
//        while (!isDoneWithStart)
//            yield return null;

//        while (!action.Invoke())
//            yield return 0.1f;
//        SetState();
//    }
//}

//public enum ChangeEventsToListen
//{
   
//}
//}

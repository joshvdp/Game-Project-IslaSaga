using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public static GlobalEvents Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public List<UnityEventWithName> Events;
    public void CallEvent(string eventName, string callerName)
    {
        FindEvent(eventName)?.Invoke();
        Debug.Log("EVENT " + eventName + " IS CALLED BY " + callerName);
    }

    public UnityEvent FindEvent(string eventName)
    {
        if (Events.Find(_ => _.myName == eventName) != null) return Events.Find(_ => _.myName == eventName).unityEvent;
        else
        {
            Debug.Log("NO EVENT WITH THE NAME " + eventName + " FOUND IN THE ANIMATION EVENTS");
            return null;
        }
    }
}
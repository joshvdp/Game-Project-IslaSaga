using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public List<UnityEventWithName> AnimationEvent;

    public void CallEvent(string eventName) => FindEvent(eventName)?.Invoke();

    public UnityEvent FindEvent(string eventName)
    {
        if (AnimationEvent.Find(_ => _.myName == eventName) != null) return AnimationEvent.Find(_ => _.myName == eventName).unityEvent;
        else
        {
            Debug.Log("NO EVENT WITH THE NAME " + eventName +" FOUND IN THE ANIMATION EVENTS");
            return null;
        }
    }
}

[Serializable]
public class UnityEventWithName
{
    public string myName;
    public UnityEvent unityEvent;
}

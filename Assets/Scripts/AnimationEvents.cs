using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public List<UnityEventWithName> AnimationEvent;

    public void CallEvent(string eventName) =>
                FindEvent(eventName)?.Invoke();

    public UnityEvent FindEvent(string eventName) => AnimationEvent.Find(_ => _.myName == eventName).unityEvent;
}

[Serializable]
public class UnityEventWithName
{
    public string myName;
    public UnityEvent unityEvent;
}

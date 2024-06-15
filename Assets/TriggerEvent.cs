    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
    public UnityEvent EventsToTrigger;
    protected virtual void OnTriggerEnter(Collider other)
    {
        EventsToTrigger?.Invoke();
        Destroy(gameObject);
    }
}

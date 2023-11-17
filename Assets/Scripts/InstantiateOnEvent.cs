using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnEvent : MonoBehaviour
{
    [SerializeField] GameObject ObjectToInstantiate;
    public void InstantiateObject() => Instantiate(ObjectToInstantiate, transform.position, Quaternion.identity);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float DestroyTimer;
    private void Awake() => Destroy(gameObject, DestroyTimer);
}

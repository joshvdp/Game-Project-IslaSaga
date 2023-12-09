using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnSpot : MonoBehaviour
{
    [SerializeField] float Force;
    [SerializeField] float Radius;
    [SerializeField] float Torque;
    [SerializeField] Transform ForcePosition;
    private void Awake()
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].AddExplosionForce(Force, ForcePosition.position, Radius);
            rbs[i].AddTorque(Random.Range(1,Torque), Random.Range(1, Torque), Random.Range(1, Torque));
        }
    }
}

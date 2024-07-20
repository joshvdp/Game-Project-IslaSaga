using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnSpot : MonoBehaviour
{
    [SerializeField] float Force;
    [SerializeField] float Radius;
    [SerializeField] float Torque;
    [SerializeField] Transform ForcePosition;
    [SerializeField] float RandomForcePositionIntensity = 0.2f;
    private void Awake()
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        Vector3 RandomizedForcePositionOffset = new Vector3(Random.Range(-RandomForcePositionIntensity, RandomForcePositionIntensity), 0f, Random.Range(-RandomForcePositionIntensity, RandomForcePositionIntensity));
        Debug.Log(RandomizedForcePositionOffset);
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].AddExplosionForce(Force, ForcePosition.position + RandomizedForcePositionOffset, Radius);
            rbs[i].AddTorque(Random.Range(1,Torque), Random.Range(1, Torque), Random.Range(1, Torque));
        }
    }
}

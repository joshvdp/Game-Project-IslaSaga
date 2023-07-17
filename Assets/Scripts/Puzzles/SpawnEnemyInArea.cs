using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puzzle;
public class SpawnEnemyInArea : MonoBehaviour
{
    [SerializeField] GameObject EnemyToSpawn;
    [SerializeField] int Amount;
    [SerializeField] float Radius;
    void Start()
    {
        if (GetComponent<PressurePlateCS>()) GetComponent<PressurePlateCS>().onPlatePressed += SpawnInArea;
    }

    private void OnDisable()
    {
        if (GetComponent<PressurePlateCS>()) GetComponent<PressurePlateCS>().onPlatePressed -= SpawnInArea;
    }
    void SpawnInArea()
    {
        for (int i = 0; i<Amount; i++)
        {
            Vector3 Location = (transform.position + Random.insideUnitSphere * Radius) + Vector3.up * 10f;
            if (Physics.Raycast(Location, -Vector3.up, out RaycastHit hitInfo, 15f))
            {
                GameObject ObjectSpawned = Instantiate(EnemyToSpawn, hitInfo.point, Quaternion.identity);
            }
        }
        GetComponent<PressurePlateCS>().onPlatePressed -= SpawnInArea;
    }

}

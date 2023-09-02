using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
public class EnemyAttackRange : MonoBehaviour
{
    [SerializeField] EnemyReferences EnemyReferencesCS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemyReferencesCS.EnemyCombat.PlayerInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            EnemyReferencesCS.EnemyCombat.PlayerInAttackRange = false;
        }
    }
}

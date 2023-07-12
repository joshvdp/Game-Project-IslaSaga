using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
using Enemy;
public class EnemyDetectCS : MonoBehaviour
{
    [SerializeField] EnemyNavScript EnemyAICS;
    [SerializeField] EnemyCombatCS EnemyCombatCS;
    [SerializeField] EnemyHpHandler EnemyHpCS;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && EnemyAICS.EnemyStateReference != EnemyState.Dead)
        {
            EnemyAICS.EnemyStateReference = EnemyState.Chasing;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && EnemyAICS.EnemyStateReference != EnemyState.Dead)
        {
            EnemyAICS.EnemyStateReference = EnemyState.Chasing;
        }
    }


}

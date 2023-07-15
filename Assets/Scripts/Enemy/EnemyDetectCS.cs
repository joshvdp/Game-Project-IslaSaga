using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
using Enemy;
public class EnemyDetectCS : MonoBehaviour
{
    [SerializeField] EnemyReferences EnemyReferencesCS;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" )
        {
            EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Chasing;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player"  && !EnemyReferencesCS.EnemyCombat.IsAttacking)
        {
            EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Chasing;
        }
    }


}

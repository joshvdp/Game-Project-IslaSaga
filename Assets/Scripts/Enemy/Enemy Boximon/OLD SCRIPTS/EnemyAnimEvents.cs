using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Enemy.AI;
using Interface;
public class EnemyAnimEvents : MonoBehaviour
{
    [SerializeField] EnemyReferences EnemyReferencesCS;
    
    void AttackPlayer()
    {
        //if(EnemyReferencesCS.EnemyCombat.PlayerInAttackRange && EnemyReferencesCS.EnemyHpCS.CurrentHealth >0) EnemyReferencesCS.EnemyAICS.Target.GetComponent<IDamageable>().Hit(EnemyReferencesCS.EnemyStatsCS.Damage);
    }
}

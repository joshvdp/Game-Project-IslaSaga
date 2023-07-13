using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Enemy.AI;
using Interface;
public class EnemyAnimEvents : MonoBehaviour
{
    [SerializeField] EnemyStats EnemyStatsCS;
    [SerializeField] EnemyNavScript EnemyAICS;
    [SerializeField] EnemyCombatCS EnemyCombat;
    
    void AttackPlayer()
    {

        if(EnemyCombat.PlayerInAttackRange) EnemyAICS.Target.GetComponent<IDamageable>().Hit(EnemyStatsCS.Damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
using Enemy.Animation;
using Enemy;
public class EnemyCombatCS : MonoBehaviour
{
    [SerializeField] EnemyNavScript EnemyAICS;
    [SerializeField] EnemyAnimationSetter EnemyAnimationCS;
    [SerializeField] EnemyHpHandler EnemyHpCS;
    public bool PlayerInAttackRange, IsAttacking;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && EnemyAICS.EnemyStateReference != EnemyState.Dead)
        {
            PlayerInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && EnemyAICS.EnemyStateReference != EnemyState.Dead)
        {
            PlayerInAttackRange = false;
        }
    }

    private void Update()
    {
        if(PlayerInAttackRange && !IsAttacking && EnemyAICS.EnemyStateReference != EnemyState.Dead)
        {
            StartCoroutine("AttackPlayer");
        }
    }


    IEnumerator AttackPlayer()
    {
        IsAttacking = true;
        EnemyAICS.AgentAI.speed = 0f;
        EnemyAnimationCS.EnemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        if(EnemyAICS.EnemyStateReference != EnemyState.Dead)
        {
            EnemyAICS.EnemyStateReference = EnemyState.Idle;
            EnemyAICS.AgentAI.speed = EnemyAICS.Speed;
            IsAttacking = false;
        }
    }
}

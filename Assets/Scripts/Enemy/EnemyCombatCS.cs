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
        if(other.tag == "Player" )
        {
            PlayerInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" )
        {
            PlayerInAttackRange = false;
        }
    }

    private void Update()
    {
        if(PlayerInAttackRange && !IsAttacking)
        {
            StartCoroutine("AttackPlayer");
        }
        if (IsAttacking) FacePlayer();
    }


    IEnumerator AttackPlayer()
    {
        IsAttacking = true;
        EnemyAICS.EnemyStateReference = EnemyState.Attacking;
        EnemyAICS.AgentAI.speed = 0f;
        EnemyAnimationCS.EnemyAnimator.SetTrigger("Attack");
        EnemyAICS.EnemyStateReference = EnemyState.Idle;

        yield return new WaitForSeconds(2f);

        EnemyAICS.EnemyStateReference = PlayerInAttackRange ? EnemyState.Idle : EnemyState.Chasing;
        EnemyAICS.AgentAI.speed = EnemyAICS.Speed;
        IsAttacking = false;
    }

    void FacePlayer()
    {
        transform.LookAt(EnemyAICS.Target);
    }
}

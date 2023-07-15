using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
using Enemy;
public class EnemyCombatCS : MonoBehaviour
{
    [SerializeField] EnemyReferences EnemyReferencesCS;

    [Header("-----  Script Variables    -----")]
    public bool PlayerInAttackRange;
    public bool IsAttacking;
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
        EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Attacking;
        EnemyReferencesCS.EnemyAICS.AgentAI.speed = 0f;
        EnemyReferencesCS.EnemyAnimationCS.EnemyAnimator.SetTrigger("Attack");
        EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Idle;

        yield return new WaitForSeconds(2f);

        EnemyReferencesCS.EnemyAICS.EnemyStateReference = PlayerInAttackRange ? EnemyState.Idle : EnemyState.Chasing;
        EnemyReferencesCS.EnemyAICS.AgentAI.speed = EnemyReferencesCS.EnemyAICS.Speed;
        IsAttacking = false;
    }

    void FacePlayer()
    {
        transform.LookAt(EnemyReferencesCS.EnemyAICS.Target);
    }
}

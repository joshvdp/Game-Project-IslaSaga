using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
using Enemy;
using SoundFX;

public class EnemyCombatCS : MonoBehaviour
{
    [SerializeField] EnemyReferences EnemyReferencesCS;

    [Header("-----  Script Variables    -----")]
    public bool PlayerInAttackRange;
    public bool IsAttacking;
    [SerializeField] float AttackAnimationLength;
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
        EnemyReferencesCS.EnemyAICS.AgentAI.isStopped = true;
        EnemyReferencesCS.EnemyAnimationCS.EnemyAnimator.SetTrigger("Attack 01");
        EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Idle; // Added to transition to idle after attacking, giving a smooth transition

        yield return new WaitForSeconds(AttackAnimationLength);

        if(!EnemyReferencesCS.EnemyHpCS.IsDead)
        {
            EnemyReferencesCS.EnemyAICS.EnemyStateReference = PlayerInAttackRange ? EnemyState.Idle : EnemyState.Chasing;
            EnemyReferencesCS.EnemyAICS.AgentAI.speed = EnemyReferencesCS.EnemyAICS.Speed;
            EnemyReferencesCS.EnemyAICS.AgentAI.isStopped = false;
            IsAttacking = false;
        }
    }

    void FacePlayer()
    {
        transform.LookAt(EnemyReferencesCS.EnemyAICS.Target);
    }
}

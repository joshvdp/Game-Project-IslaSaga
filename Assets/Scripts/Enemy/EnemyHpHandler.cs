using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;
using Enemy.Animation;
using Enemy.AI;
namespace Enemy
{
    public class EnemyHpHandler : MonoBehaviour, IDamageable
    {
        [Header("References")]
        [SerializeField] EnemyReferences EnemyReferencesCS;


        [Header("-----  Script Variables    -----")]
        [SerializeField] float EnemyHp;
        [SerializeField] float HitStunTime;
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public bool IsDead = false;

        private void Start()
        {
            CurrentHealth = EnemyReferencesCS.EnemyStatsCS.Health;
            EnemyHp = CurrentHealth;
        }
        public void Hit(float Damage)
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= Damage;
                EnemyHp = CurrentHealth;
                EnemyReferencesCS.EnemyAnimationCS.EnemyAnimator.SetTrigger("TookDamage");
                CheckHp();
                StartCoroutine(HitStun());
            }
        }

        void CheckHp()
        {
            if (CurrentHealth <= 0f)
            {
                EnemyHp = CurrentHealth;
                Death();
            }
        }

        public void Death()
        {
            IsDead = true;
            EnemyReferencesCS.EnemyAnimationCS.enabled = false;
            EnemyReferencesCS.EnemyAICS.enabled = false;
            EnemyReferencesCS.EnemyCombat.enabled = false;
            EnemyReferencesCS.EnemyAttackRange.enabled = false;
            EnemyReferencesCS.EnemyDetect.enabled = false;
            EnemyReferencesCS.EnemyMainCollider.enabled = false;

            EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Dead;
            EnemyReferencesCS.EnemyAnimationCS.EnemyAnimator.SetTrigger("Death");
        }

        IEnumerator HitStun()
        {
            var StateBeforeStunned = EnemyReferencesCS.EnemyAICS.EnemyStateReference;
            EnemyReferencesCS.EnemyAICS.AgentAI.speed = 0f;
            EnemyReferencesCS.EnemyAICS.AgentAI.isStopped = true;
            EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Idle;
            yield return new WaitForSeconds(HitStunTime);
            EnemyReferencesCS.EnemyAICS.EnemyStateReference = StateBeforeStunned;
            EnemyReferencesCS.EnemyAICS.AgentAI.speed = EnemyReferencesCS.EnemyAICS.Speed;
            EnemyReferencesCS.EnemyAICS.AgentAI.isStopped = false;
        }
    }
}


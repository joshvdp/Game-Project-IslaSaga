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
        [SerializeField] EnemyStats EnemyStatsCS;
        [SerializeField] EnemyAnimationSetter EnemyAnimation;
        [SerializeField] EnemyNavScript EnemyAICS;
        [SerializeField] EnemyCombatCS EnemyCombatCS;
        [SerializeField] EnemyDetectCS EnemyDetect;
        [SerializeField] SphereCollider AttackRangeColl;
        [SerializeField] CapsuleCollider MainCollider;


        [Header("Stats")]
        [SerializeField] float EnemyHp;
        [SerializeField] float HitStunTime;
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        public bool IsDead = false;

        private void Start()
        {
            CurrentHealth = EnemyStatsCS.Health;
            EnemyHp = CurrentHealth;
        }
        public void Hit(float Damage)
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth -= Damage;
                EnemyHp = CurrentHealth;
                EnemyAnimation.EnemyAnimator.SetTrigger("TookDamage");
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
            EnemyAnimation.enabled = false;
            EnemyAICS.enabled = false;
            EnemyCombatCS.enabled = false;
            AttackRangeColl.enabled = false;
            EnemyDetect.enabled = false;
            MainCollider.enabled = false;

            EnemyAICS.EnemyStateReference = EnemyState.Dead;
            EnemyAnimation.EnemyAnimator.SetTrigger("Death");
        }

        IEnumerator HitStun()
        {
            var StateBeforeStunned = EnemyAICS.EnemyStateReference;
            EnemyAICS.AgentAI.speed = 0f;
            EnemyAICS.AgentAI.isStopped = true;
            EnemyAICS.EnemyStateReference = EnemyState.Idle;
            yield return new WaitForSeconds(HitStunTime);
            EnemyAICS.EnemyStateReference = StateBeforeStunned;
            EnemyAICS.AgentAI.speed = EnemyAICS.Speed;
            EnemyAICS.AgentAI.isStopped = false;
        }
    }
}


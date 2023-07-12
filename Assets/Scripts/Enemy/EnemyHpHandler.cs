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
        [SerializeField] EnemyAnimationSetter EnemyAnimation;
        [SerializeField] EnemyNavScript EnemyAICS;
        [SerializeField] EnemyCombatCS EnemyCombatCS;
        [SerializeField] SphereCollider AttackRangeColl;
        [SerializeField] CapsuleCollider MainCollider;


        [Header("Stats")]
        [SerializeField] float EnemyHp;
        [SerializeField] float HitStunTime;
        public bool IsDead = false;
        void Start()
        {

        }

        public void Hit(float Damage)
        {
            if (EnemyHp > 0)
            {
                EnemyHp -= Damage;
                EnemyAnimation.EnemyAnimator.SetTrigger("TookDamage");
                CheckHp();

                StartCoroutine(HitStun());
            }
        }

        void CheckHp()
        {
            if (EnemyHp <= 0f)
            {
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


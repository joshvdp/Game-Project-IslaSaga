using System.Collections;
using UnityEngine;
using Interface;
using Enemy.AI;
using VFX;
using SoundFX;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyHpHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] EnemyReferences EnemyReferencesCS;


        [Header("-----  Script Variables    -----")]
        [Header("Stats")]
        [SerializeField] float EnemyHp;
        [SerializeField] float HitStunTime;
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
        [field: SerializeField] public UnityEvent onDeath { get; set; }
        public bool IsDamageable { get; set; }

        public bool IsDead = false;
        [SerializeField] float DeathAnimLength;
        [Header("Knockback Variables")]
        public bool CanBeHitBack;
        public bool IsBeingKnockbacked;
        public float KnockbackTime;
        public float KnockbackAmount;

        [Header("Hit Effect Variables")]
        [SerializeField] float EffectTime;
        [SerializeField] Renderer[] MainRenderers;
        [SerializeField] Color HitColor;
        [SerializeField] Color DefaultColor;

        private void Start()
        {
            CurrentHealth = EnemyReferencesCS.EnemyStatsCS.Health;
            EnemyHp = CurrentHealth;
        }

        public void Hit(float Damage)
        {
            if (CurrentHealth > 0)
            {
                if (CanBeHitBack && !IsBeingKnockbacked)
                {
                    StartCoroutine("Knockback");
                }
                CurrentHealth -= Damage;
                EnemyHp = CurrentHealth;
                EnemyReferencesCS.EnemyAnimationCS.EnemyAnimator.SetTrigger("TookDamage");
                CheckHp();
                StartCoroutine(HitStun());
                StartCoroutine(HitEffect());
                VFXManager.Instance.SpawnDmgPopup(transform.position, Damage, 1, Color.red);
                EnemySFX.onHit?.Invoke();
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
            EnemySFX.onDeath?.Invoke();
            EnemyReferencesCS.EnemyAICS.AgentAI.speed = 0f;
            EnemyReferencesCS.EnemyAICS.Target = null;
            EnemyReferencesCS.EnemyAnimationCS.enabled = false;
            EnemyReferencesCS.EnemyAICS.enabled = false;
            EnemyReferencesCS.EnemyCombat.enabled = false;
            EnemyReferencesCS.EnemyAttackRange.enabled = false;
            EnemyReferencesCS.EnemyDetect.enabled = false;
            EnemyReferencesCS.EnemyMainCollider.enabled = false;

            EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Dead;
            EnemyReferencesCS.EnemyAnimationCS.EnemyAnimator.SetTrigger("Die");

            Destroy(gameObject, DeathAnimLength);
        }

        IEnumerator HitStun()
        {
            //sfx here
            var StateBeforeStunned = EnemyReferencesCS.EnemyAICS.EnemyStateReference;
            EnemyReferencesCS.EnemyAICS.AgentAI.speed = 0f;
            EnemyReferencesCS.EnemyAICS.AgentAI.isStopped = true;
            EnemyReferencesCS.EnemyAICS.EnemyStateReference = EnemyState.Idle;
            yield return new WaitForSeconds(HitStunTime);
            if(!IsDead && !EnemyReferencesCS.EnemyCombat.IsAttacking)
            {
                EnemyReferencesCS.EnemyAICS.EnemyStateReference = StateBeforeStunned;
                EnemyReferencesCS.EnemyAICS.AgentAI.speed = EnemyReferencesCS.EnemyAICS.Speed;
                EnemyReferencesCS.EnemyAICS.AgentAI.isStopped = false;
            }
        }
        IEnumerator Knockback()
        {
            //EnemySFX.onHit?.Invoke();
            IsBeingKnockbacked = true;
            float TimeElapsed = 0;
            Vector3 KnockbackDir = EnemyReferencesCS.EnemyAICS.Target.forward;


            EnemyReferencesCS.EnemyAICS.enabled = false;
            EnemyReferencesCS.EnemyCombat.enabled = false;

            while (TimeElapsed < KnockbackTime)
            {
                TimeElapsed += Time.deltaTime;
                transform.Translate(KnockbackDir.normalized * KnockbackAmount * Time.deltaTime, Space.World);
                yield return null;
            }

            if(!IsDead)
            {
                EnemyReferencesCS.EnemyAICS.enabled = true;
                EnemyReferencesCS.EnemyCombat.enabled = true;
            }
            IsBeingKnockbacked = false;
            yield break;
        }
        IEnumerator HitEffect()
        {
            //sfx here
            for(int i = 0; i<MainRenderers.Length; i++)
            {
                MainRenderers[i].material.color = HitColor;
            }
            yield return new WaitForSeconds(EffectTime);

            for (int i = 0; i < MainRenderers.Length; i++)
            {
                MainRenderers[i].material.color = DefaultColor;
            }
        }
    }
}


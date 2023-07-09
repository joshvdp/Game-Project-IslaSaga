using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
namespace Enemy.Animation
{
    public class EnemyAnimationSetter : MonoBehaviour
    {
        [Header("References")]
        public Animator EnemyAnimator;
        [SerializeField] EnemyNavScript EnemyAICS;
        [SerializeField] EnemyHpHandler EnemyHpCS;

        private void Update()
        {
            if (EnemyAICS.EnemyStateReference != EnemyState.Dead)
            {
                AnimationSetter();
            }
        }

        void AnimationSetter()
        {
            EnemyAnimator.SetBool("IsIdle", (EnemyAICS.EnemyStateReference == EnemyState.Idle) ? true:false);
            EnemyAnimator.SetBool("IsChasing", (EnemyAICS.EnemyStateReference == EnemyState.Chasing) ? true : false);
            EnemyAnimator.SetBool("IsPatrolling", (EnemyAICS.EnemyStateReference == EnemyState.Patrolling) ? true : false);

        }

    }
}


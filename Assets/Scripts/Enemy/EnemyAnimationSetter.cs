using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
namespace Enemy.Animation
{
    public class EnemyAnimationSetter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] EnemyReferences EnemyReferencesCS;


        [Header("-----  Script Variables    -----")]
        public Animator EnemyAnimator;

        private void Update()
        {
            AnimationSetter();
        }

        void AnimationSetter()
        {
            EnemyAnimator.SetBool("IsIdle", (EnemyReferencesCS.EnemyAICS.EnemyStateReference == EnemyState.Idle) ? true:false);
            EnemyAnimator.SetBool("IsChasing", (EnemyReferencesCS.EnemyAICS.EnemyStateReference == EnemyState.Chasing) ? true : false);
            EnemyAnimator.SetBool("IsPatrolling", (EnemyReferencesCS.EnemyAICS.EnemyStateReference == EnemyState.Patrolling) ? true : false);
        }

    }
}


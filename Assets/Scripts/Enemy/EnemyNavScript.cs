using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.AI
{
    public enum EnemyState
    {
        Idle,
        Chasing,
        Patrolling,
        Attacking,
        Dead
    }
    public class EnemyNavScript : MonoBehaviour
    {
        [Header("References")]

        [SerializeField] EnemyStats EnemyStatsCS;
        public EnemyState EnemyStateReference;
        public NavMeshAgent AgentAI;
        [HideInInspector] public Transform Target;
        [SerializeField] EnemyHpHandler EnemyHpCS;
        [SerializeField] EnemyCombatCS EnemyCombat;
        [Header("Stats")]

        public float Speed;
        private void Start()
        {
            Target = GameObject.Find("Player 1").transform;

            Speed = EnemyStatsCS.Speed;

            AgentAI.speed = Speed;
        }

        void Update()
        {
            if (EnemyStateReference == EnemyState.Chasing && !EnemyCombat.IsAttacking)
            {
                ChasePlayer();
            }

        }
        void ChasePlayer()
        {
            AgentAI.destination = Target.position;
        }
    }
}
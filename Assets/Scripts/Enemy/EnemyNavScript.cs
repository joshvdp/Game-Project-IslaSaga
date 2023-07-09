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
        public EnemyState EnemyStateReference;
        public NavMeshAgent AgentAI;
        Transform Target;
        [SerializeField] EnemyHpHandler EnemyHpCS;

        [Header("Stats")]

        public float Speed;
        private void Start()
        {
            Target = GameObject.Find("Player 1").transform;
            AgentAI.speed = Speed;
        }

        void Update()
        {
            if (EnemyStateReference != EnemyState.Dead)
            {
                if (EnemyStateReference == EnemyState.Chasing)
                {
                    ChasePlayer();
                }
            }

        }
        void ChasePlayer()
        {
            AgentAI.destination = Target.position;
        }

    }
}

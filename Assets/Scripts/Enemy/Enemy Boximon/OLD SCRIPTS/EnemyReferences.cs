using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.AI;
using Enemy.Animation;
namespace Enemy
{
    public class EnemyReferences : MonoBehaviour
    {
        public EnemyStats EnemyStatsCS;

        public EnemyCombatCS EnemyCombat;
        public EnemyNavScript EnemyAICS;
        public EnemyHpHandler EnemyHpCS;
        public EnemyAnimationSetter EnemyAnimationCS;

        public EnemyDetectCS EnemyDetect;

        public CapsuleCollider EnemyMainCollider;
        public SphereCollider EnemyAttackRange;
    }
}


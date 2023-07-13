using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Enemy Stats", menuName = "Enemy Stats")]
    public class EnemyStats : ScriptableObject
    {
        public float Health;
        public float Speed;
        public float Damage;
    }
}


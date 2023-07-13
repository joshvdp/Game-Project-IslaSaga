using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public interface IDamageable
    {
        float MaxHealth { get; set;}
        float CurrentHealth { get; set; }
        void Hit(float Damage);
        void Death();
    }
}
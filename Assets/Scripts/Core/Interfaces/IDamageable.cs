using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Interface
{
    public interface IDamageable
    {
        UnityEvent onDeath { get; set; }
        float MaxHealth { get; set;}
        float CurrentHealth { get; set; }

        bool IsDamageable { get; set; }
        void Hit(float Damage);

    }

}
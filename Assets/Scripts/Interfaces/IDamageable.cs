using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interface
{
    public interface IDamageable
    {
        void Hit(float Damage);
        void Death();
    }
}
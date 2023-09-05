using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class AttackCollidersHandler : MonoBehaviour
    {
        AttackCollider[] Colliders => GetComponentsInChildren<AttackCollider>();

        public AttackCollider FindCollider(string colliderName)
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                if (Colliders[i].name == colliderName) return Colliders[i];
            }
            Debug.Log("ATTACK COLLIDER NAME '" + colliderName + "' IS NOT FOUND");
            return null;
        }
    }
}


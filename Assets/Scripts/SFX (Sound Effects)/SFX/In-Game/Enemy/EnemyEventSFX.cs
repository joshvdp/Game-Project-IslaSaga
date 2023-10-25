using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class EnemyEventSFX : MonoBehaviour
    {
        public GameObject OnHitting, OnDeath;

        public void Hitting()
        {
            Debug.Log("HIT");
            Instantiate(OnHitting, transform.position, transform.rotation);
        }

        public void Died()
        {
            Debug.Log("Minion Dies");
        }
    }
}


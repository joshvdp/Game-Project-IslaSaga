using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class MinionEnemyAudio : MonoBehaviour
    {
        public GameObject OnHitting, OnDeath;
        public void Hitting()
        {
            Instantiate(OnHitting, transform.position, transform.rotation);
        }

        public void Died()
        {
            Instantiate(OnDeath, transform.position, transform.rotation);
        }
    }
}


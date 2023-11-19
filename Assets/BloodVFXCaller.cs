using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class BloodVFXCaller : MonoBehaviour
    {
        public void SpawnBlood()
        {
            BloodParticle.Hit?.Invoke();
        }
        public void DespawnBlood()
        {
            BloodParticle.HitDone?.Invoke();
        }
    }
}
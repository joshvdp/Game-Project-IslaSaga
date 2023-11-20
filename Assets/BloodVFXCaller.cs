using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class BloodVFXCaller : MonoBehaviour
    {
        public void SpawnBlood()
        {
            Debug.Log("Invoke");
            BloodParticle.Hit?.Invoke();
        }
        public void DespawnBlood()
        {
            BloodParticle.HitDone?.Invoke();
        }
    }
}
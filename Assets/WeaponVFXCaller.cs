using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX 
{ 
    public class WeaponVFXCaller : MonoBehaviour
    {
        public void SpawnParticle()
        {
            //Debug.Log("Spawn Particle");
            WeaponParticle.Swing?.Invoke();
        }

        public void DespawnParticle()
        {
            WeaponParticle.SwingDone?.Invoke();
        }
    }
}

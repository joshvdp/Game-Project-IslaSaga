using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX 
{ 
    public class WeaponVFXCaller : MonoBehaviour
    {
        public void SpawnParticle()
        {
            WeaponParticle.Swing?.Invoke();
        }

        public void DespawnParticle()
        {

            StartCoroutine(WaitForSeconds());
            WeaponParticle.SwingDone?.Invoke();
        }

        IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class SprintParticleCaller : MonoBehaviour
    {
        public GameObject SprintParticle;

        public void Sprinting()
        {
            SprintParticle.SetActive(true);
        }
        public void DoneSprinting()
        {
            SprintParticle.SetActive(false);
        }
    }
}

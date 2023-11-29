using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class TorchFireSpawner : MonoBehaviour
    {
        public GameObject spotlight;
        public ParticleSystem torchFire;

        public void Start()
        {
            Instantiate(torchFire, spotlight.transform.position, spotlight.transform.rotation);
        }

    }
}

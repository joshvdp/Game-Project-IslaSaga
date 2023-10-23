using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class VaseSFX : MonoBehaviour
    {
        public GameObject Wrecked;
        public void Destroyed()
        {
            Debug.Log("Vase Cracked");
            Instantiate(Wrecked, transform.position, transform.rotation);
        }
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class VaseSFX : MonoBehaviour
    {
        public GameObject Destroy;
        public void Destroyed()
        {
            Debug.Log("Destroyed");
            Instantiate(Destroy, transform.position, transform.rotation);
        }
    }
}



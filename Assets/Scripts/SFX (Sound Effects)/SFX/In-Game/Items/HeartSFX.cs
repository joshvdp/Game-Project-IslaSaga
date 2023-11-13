using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class HeartSFX : MonoBehaviour
    {
        public GameObject Picked;
        public void Grab()
        {
            //Debug.Log("Picked-Up");
            Instantiate(Picked, transform.position, transform.rotation);
        }
    }
}



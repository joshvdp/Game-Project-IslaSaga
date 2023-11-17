using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVisualEvents
{
    public class Pickups : MonoBehaviour
    {
        public GameObject Sound, Particle;
        public void Grab()
        {
            //Debug.Log("Picked-Up");
            Instantiate(Sound, transform.position, transform.rotation);
            Instantiate(Particle, transform.position, transform.rotation);
        }
    }
}



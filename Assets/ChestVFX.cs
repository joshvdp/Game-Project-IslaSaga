using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVisualEvents 
{ 
    public class ChestVFX : MonoBehaviour
    {  
        public GameObject Particle;
        public void ChestOpen()
        {
          Debug.Log("Chest Open");
          //Instantiate(Open, transform.position, transform.rotation);
          Instantiate(Particle, Particle.transform.position, Particle.transform.rotation);
        }
    }
}

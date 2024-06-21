using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVisualEvents 
{ 
    public class ChestSVFX : MonoBehaviour
    {  
        public GameObject SpawnPoint, Particle, OpenSound;
        public void ChestOpen()
        {
          //Debug.Log("Chest Open");
          //Instantiate(Open, transform.position, transform.rotation);
          Instantiate(Particle, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
          Instantiate(OpenSound, transform.position, transform.rotation);
        }
    }
}

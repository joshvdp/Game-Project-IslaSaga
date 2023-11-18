using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioVisualEvents 
{ 
    public class ChestVFX : MonoBehaviour
    {  
        public GameObject SpawnPoint, Particle;
        public void ChestOpen()
        {
          Debug.Log("Chest Open");
          //Instantiate(Open, transform.position, transform.rotation);
          Instantiate(Particle, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        }
    }
}

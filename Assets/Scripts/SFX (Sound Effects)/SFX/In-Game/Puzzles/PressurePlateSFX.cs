using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class PressurePlateSFX : MonoBehaviour
    {
        private GameObject Triggered, NotTriggered;
        
        public void Entry()
        {
            Debug.Log("On");
        }

        public void Leave()
        {
            Debug.Log("Off");
        }
    }

}

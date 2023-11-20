using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class PressurePlateSFX : MonoBehaviour
    {
        public GameObject Triggered, Untriggered;
        
        public void Entry()
        {
            Instantiate(Triggered, transform.position, transform.rotation);
        }

        public void Leave()
        {
            Instantiate(Untriggered, transform.position, transform.rotation);
        }
    }

}

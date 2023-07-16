using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalSoundFX
{
    public class GlobalSFX : MonoBehaviour
    {
        public delegate void statusEvent();
        public static statusEvent onEnter, onExit;

        public GameObject PressurePlate0, PressurePlate1;

        private PressurePlateCS sound;

        private void OnEnable()
        {
            onEnter += pressurePlateOn;
            onExit += pressurePlateOff;
        }

        private void OnDisable()
        {
            onEnter -= pressurePlateOn;
            onExit -= pressurePlateOff;
        }
        private void Start()
        {
            sound = GetComponent<PressurePlateCS>();
        }

        public void pressurePlateOff()
        {
            Debug.Log("Pressure = 0");
            GameObject play = Instantiate(PressurePlate0, transform.position, transform.rotation);
        }
        public void pressurePlateOn ()
        {
            Debug.Log("Pressure = 1");
            GameObject play = Instantiate(PressurePlate1, transform.position, transform.rotation);
        }
       



    }


}

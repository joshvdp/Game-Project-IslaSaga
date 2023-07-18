using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puzzle;

namespace SoundFX
{
    public class GlobalSFX : MonoBehaviour
    {
        public delegate void statusEvent();
        public static statusEvent onEnter, onExit, gateOpen, gateClose;

        public GameObject PressurePlate0, PressurePlate1, Gate0, Gate1;

        private PressurePlateCS sound;

        private void OnEnable()
        {
            onEnter += pressurePlateOn;
            onExit += pressurePlateOff;
            gateOpen += openGate;
            gateClose += closeGate;
        }

        private void OnDisable()
        {
            onEnter -= pressurePlateOn;
            onExit -= pressurePlateOff;
            gateOpen -= openGate;
            gateClose -= closeGate;
        }
        private void Start()
        {
            sound = GetComponent<PressurePlateCS>();
        }

        public void pressurePlateOff()
        {
            GameObject play = Instantiate(PressurePlate0, transform.position, transform.rotation);
        }
        public void pressurePlateOn ()
        {
            GameObject play = Instantiate(PressurePlate1, transform.position, transform.rotation);
        }
        public void openGate()
        {
            GameObject play = Instantiate(Gate1, transform.position, transform.rotation);
        }
        public void closeGate()
        {
            GameObject play = Instantiate(Gate0, transform.position, transform.rotation);
        }
       



    }


}

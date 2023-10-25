using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Puzzle;
using AudioSoundEvents; 
using UnityEngine.Events;
namespace Puzzle
{
    public class PressurePlateCS : MonoBehaviour
    {
        Vector3 StartingPos;
        public delegate void OnPlatePressed();
        public OnPlatePressed onPlatePressed;

        [Header("References")]
        [SerializeField] PressurePlateGateCS MasterGate;
        [SerializeField] Renderer ObjectRenderer;
        [Header("Variables")]
        [SerializeField] float PressureSpeed = 1;
        [SerializeField] int ObjectsOnTop = 0;

        [SerializeField] float MaxYDownDistance;
        public bool IsPressed => transform.localPosition.y <= MaxYDownDistance;
        public bool EventFired = false;
        public UnityEvent Activated, Deactivated;
        void Start()
        {
            MaxYDownDistance = transform.localPosition.y - MaxYDownDistance;
            StartingPos = transform.position;
        }
        private void OnTriggerEnter(Collider other)
        {
            Activated.Invoke();
            if (other.tag == "Player" || other.tag == "Moveable") ObjectsOnTop++;
        }

        private void OnTriggerExit(Collider other)
        {
            Deactivated.Invoke();
            if (other.tag == "Player" || other.tag == "Moveable") ObjectsOnTop--;
        }

        private void Update()
        {
            if (ObjectsOnTop>0 && transform.localPosition.y > MaxYDownDistance) Descend();
            else if (ObjectsOnTop <= 0 && transform.position.y < StartingPos.y) Ascend();
            if(IsPressed && !EventFired)
            {
                onPlatePressed?.Invoke();
                EventFired = true;
            }
        }
        void Ascend() => transform.position += Vector3.up * PressureSpeed * Time.deltaTime;
        void Descend() => transform.position -= Vector3.up * PressureSpeed * Time.deltaTime;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Puzzle;
using AudioSoundEvents; 
namespace Puzzle
{
    public class PressurePlateCS : MonoBehaviour
    {
        Vector3 StartingPos;
        Vector3 MaxDistancePosition;
        public delegate void OnPlatePressed();
        public OnPlatePressed onPlatePressed;

        [Header("References")]
        [SerializeField] PressurePlateGateCS MasterGate;
        [SerializeField] Renderer ObjectRenderer;
        [Header("Variables")]
        [SerializeField] float PressureSpeed;
        [SerializeField] int ObjectsOnTop = 0;

        [SerializeField] float MaxYDownDistance;
        public bool IsPressed;
        public bool EventFired = false;
        void Start()
        {
            StartingPos = transform.position;
            MaxDistancePosition = new Vector3(StartingPos.x, StartingPos.y - MaxYDownDistance, StartingPos.z);
        }
        private void OnTriggerEnter(Collider other)
        {
            GlobalSFX.onEnter?.Invoke();
            if (other.tag == "Player" || other.tag == "Moveable")
            {
                ObjectsOnTop++;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GlobalSFX.onExit?.Invoke();
            if (other.tag == "Player" || other.tag == "Moveable")
            {
                ObjectsOnTop--;
            }
        }

        private void Update()
        {
            if (ObjectsOnTop > 0)
            {
                ObjectRenderer.material.color = Color.red;
                GoDown();
            }

            if (ObjectsOnTop <= 0)
            {
                ObjectRenderer.material.color = Color.green;
                GoUp();
            }

            if (transform.position.y <= MaxDistancePosition.y && !IsPressed)
            {
                if (!EventFired) onPlatePressed.Invoke();
                EventFired = true;
                IsPressed = true;
            }

            if (transform.position.y >= StartingPos.y && IsPressed)
            {
                IsPressed = false;
            }

        }

        void GoDown()
        {
            if (transform.position.y >= MaxDistancePosition.y)
                transform.Translate(Vector3.down * Time.deltaTime * PressureSpeed, Space.World);
        }

        void GoUp()
        {
            if (transform.position.y <= StartingPos.y)
                transform.Translate(Vector3.up * Time.deltaTime * PressureSpeed, Space.World);
        }
    }
}


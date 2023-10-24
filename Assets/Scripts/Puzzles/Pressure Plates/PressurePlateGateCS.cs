using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puzzle;
namespace Puzzle
{
    public class PressurePlateGateCS : MonoBehaviour
    {
        Vector3 StartingPosition;

        [Header("Pressure Plates Connected")]
        public PressurePlateCS[] ConnectedPressurePlates;

        public bool AllPlatesPressed = false;

        [Header("For Disappearing")]
        [SerializeField] Renderer GateRenderer;
        [SerializeField] Collider GateCollider;

        [Header("For Opening")]
        [SerializeField] float OpenSpeed;
        [SerializeField] Vector3 OpenPosition;

        [Header("Choose 1 for Opening")]
        [SerializeField] bool MustOpen;
        [SerializeField] bool MustDisappear;

        private void Awake()
        {
            StartingPosition = transform.localPosition;
        }
        private void Update()
        {
            CheckPressedPressurePlates();
        }

        void DoAction()
        {
            if (MustOpen && Vector3.Distance(transform.localPosition, OpenPosition) > 0) GoToStartingPosition();
            if (MustDisappear) Disappear();
        }

        public void CheckPressedPressurePlates()
        {
            int PressedPressurePlates = 0;
            for (int i = 0; i < ConnectedPressurePlates.Length; i++)
            {
                if (ConnectedPressurePlates[i].IsPressed == true)
                {
                    PressedPressurePlates++;
                }
            }
            if (PressedPressurePlates >= ConnectedPressurePlates.Length)
            {
                AllPlatesPressed = true;
                DoAction();
            }
            else
            {
                Appear();
                if (Vector3.Distance(transform.localPosition, StartingPosition) > 0) GoToOpenPosition();
            }
        }

        void GoToOpenPosition()
        {
            transform.localPosition += (StartingPosition - transform.localPosition).normalized * OpenSpeed * Time.deltaTime;
        }

        void GoToStartingPosition()
        {
            transform.localPosition += (OpenPosition - transform.localPosition).normalized * OpenSpeed * Time.deltaTime;
        }

        void Disappear()
        {
            GateRenderer.enabled = false;
            GateCollider.enabled = false;
        }

        void Appear()
        {
            AllPlatesPressed = false;
            GateRenderer.enabled = true;
            GateCollider.enabled = true;
        }
    }

}

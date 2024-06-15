using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace CameraSystem
{
    [RequireComponent(typeof(Collider))]
    public class RoomCameraTrigger : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera roomCamera;
        [SerializeField] private UnityEvent onEnter;
        
        protected void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            RoomCameraManager.Instance.SetAsCurrentCamera(roomCamera);
            onEnter?.Invoke();
        }
    }
}
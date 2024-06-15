using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace CameraSystem
{
    public class RoomCameraManager : MonoBehaviour
    {
        public static RoomCameraManager Instance { get; private set; }
        
        [field: SerializeField, InfoBox("The first item will be the set default active.")]
        public List<CinemachineVirtualCamera> RoomCameras { get; private set; }
        [SerializeField] public UnityEvent<CinemachineVirtualCamera> onChangeCamera;

        public CinemachineVirtualCamera CurrentCameraTrigger { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            ActiveAndInactiveCameras();
        }

        public void SetAsCurrentCamera(CinemachineVirtualCamera roomCamera)
        {
            if (CurrentCameraTrigger == roomCamera || !RoomCameras.Contains(roomCamera))
                return;
            
            roomCamera.gameObject.SetActive(true);
            
            if (CurrentCameraTrigger != null)
                CurrentCameraTrigger.gameObject.SetActive(false);
            
            CurrentCameraTrigger = roomCamera;
            onChangeCamera?.Invoke(CurrentCameraTrigger);
        }
        
        [Button]
        public void ActiveAndInactiveCameras()
        {
            for (int i = 0; i < RoomCameras.Count; i++)
                RoomCameras[i].gameObject.SetActive(i == 0);
        }
    }
}
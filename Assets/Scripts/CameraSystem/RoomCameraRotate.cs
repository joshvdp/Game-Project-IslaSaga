using System;
using System.Collections;
using Cinemachine;
using Manager;
using Player.Controls;
using UnityEngine;

namespace CameraSystem
{
    public class RoomCameraRotate : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private ControlBindings controls;
        [SerializeField] private float defaultTouchSensitivity = 20;

        private static Action<Transform> OnRotate; 

        private IEnumerator Start()
        {
            if (MainManager.Instance == null)
                return PCCamera();
            
            return MainManager.Instance.Settings.PlatformType switch
            {
                PlatformType.PC => PCCamera(),
                PlatformType.Mobile => MobileCamera()
            };
        }

        private void OnEnable() => OnRotate += CopyRotation;
        private void OnDisable() => OnRotate -= CopyRotation;

        private void CopyRotation(Transform sender)
        {
            if (sender == transform)
                return;
            
            transform.rotation = sender.rotation;
        }

        private IEnumerator PCCamera()
        {
            repeat:
            yield return new WaitUntil(IsKeyInAction);
            Vector3 touchPosition = Input.mousePosition;
            
            float lookSensitivity = SettingsHandler.Instance ? SettingsHandler.Instance.settingsData.LookSensitivityValue : defaultTouchSensitivity;
            do
            {
                float touchDistance = (Input.mousePosition - touchPosition).magnitude;
                int direction = 
                    touchPosition.x < Input.mousePosition.x ? 1 : 
                    touchPosition.x > Input.mousePosition.x ? -1 : 0;
                float power = lookSensitivity * Time.deltaTime * touchDistance * direction;

                transform.Rotate(Vector3.forward, power);
                OnRotate?.Invoke(transform); // Send new rotation to all other CameraRotators
                
                touchPosition = Input.mousePosition;
                yield return null;
            } while (IsKeyInAction());
            
            goto repeat;
            bool IsKeyInAction() => Input.GetKey(controls.CameraRotateKey);
        }
        
        private IEnumerator MobileCamera()
        {
            yield return null;
        }

        private void OnValidate()
        {
            if (virtualCamera == null)
                virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    }
}

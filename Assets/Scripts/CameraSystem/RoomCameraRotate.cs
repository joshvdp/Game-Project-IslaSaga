using System;
using System.Collections;
using Cinemachine;
using Manager;
using Mobile;
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

        private float LookSensitivity => SettingsHandler.Instance ? SettingsHandler.Instance.settingsData.LookSensitivityValue : defaultTouchSensitivity;

        private IEnumerator Start()
        {
            if (MainManager.Instance == null)
                return PCCamera();
            
            return MainManager.Instance.Settings.PlatformType switch
            {
                PlatformType.PC => PCCamera(),
                PlatformType.Mobile => MobileCamera(),
                _ => throw new NotImplementedException()
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
            pcRepeat:
            yield return new WaitUntil(IsKeyInAction);
            Vector3 touchPosition = Input.mousePosition;
            
            do
            {
                float touchDistance = (Input.mousePosition - touchPosition).magnitude;
                int direction = 
                    touchPosition.x < Input.mousePosition.x ? 1 : 
                    touchPosition.x > Input.mousePosition.x ? -1 : 0;
                float power = LookSensitivity * touchDistance * direction * Time.deltaTime;

                transform.Rotate(Vector3.forward, power);
                OnRotate?.Invoke(transform); // Send new rotation to all other CameraRotators
                
                touchPosition = Input.mousePosition;
                yield return null;
            } while (IsKeyInAction());
            
            goto pcRepeat;
            bool IsKeyInAction() => Input.GetKey(controls.CameraRotateKey);
        }
        
        private IEnumerator MobileCamera()
        {
            FixedTouchField touchField = null;
            yield return new WaitUntil(() =>
            {
                if (MobileUIInputHandler.instance == null)
                    return false;

                touchField = MobileUIInputHandler.instance.TouchField;
                return true;
            });

            mobileRepeat:
            
            float power = LookSensitivity * touchField.TouchDist.x * Time.deltaTime;
            transform.Rotate(Vector3.forward, power);
            OnRotate?.Invoke(transform); // Send new rotation to all other CameraRotators
            yield return null;

            goto mobileRepeat;
        }

        private void OnValidate()
        {
            if (virtualCamera == null)
                virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    }
}

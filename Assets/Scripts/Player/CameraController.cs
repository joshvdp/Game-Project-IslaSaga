using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cam.Controls
{
    public class CameraController : MonoBehaviour
    {
        [Header("Essentials")]
        [SerializeField] Transform target;
        [SerializeField] ControlBindings Controls;

        [Header("Follow Variables")]
        [SerializeField] Vector3 offset;
        [SerializeField] float FollowSpeed = 0.2f;
        [SerializeField] bool LookAtPlayer;


        [Header("Rotate Variables")]
        [SerializeField] float CamRotationSpeed;


        [Header("Zoom Variables")]
        [SerializeField] float MinYOffset, MinZOffset;
        [SerializeField] float MaxYOffset, MaxZOffset;

        [SerializeField] float OffsetIndicator;
        [SerializeField] float ZoomInSpeed;
        Vector3 ZoomOffset;

        [Header("Collision Variables")]
        [SerializeField] LayerMask RayHittableLayers;

        private void Update()
        {
            RotateCamera();
            ZoomInOrOut();
            CameraCollisionHandler();
        }
        private void FixedUpdate()
        {
            FollowPlayerPosition();
        }

        void FollowPlayerPosition()
        {
            if(LookAtPlayer) transform.LookAt(target);
            Vector3 Forward1 = transform.forward;
            Vector3 Forward2 = target.forward;
            Forward1.y = 0;
            Forward2.y = 0;

            ZoomOffset.z *= Vector3.Dot(Forward1, Forward2) ; // To have equal zoom on z position for both forwards.

            transform.position = Vector3.Lerp(transform.position, target.position + offset + ZoomOffset, FollowSpeed * Time.deltaTime);
        }

        void ZoomInOrOut()
        {
            OffsetIndicator = Mathf.Clamp(OffsetIndicator, 0, 1);
            OffsetIndicator += Input.mouseScrollDelta.y * ZoomInSpeed;
            ZoomOffset.y = Mathf.Lerp(MaxYOffset, MinYOffset, OffsetIndicator / 1);
            ZoomOffset.z = Mathf.Lerp(MaxZOffset, MinZOffset, OffsetIndicator / 1);
        }

        void RotateCamera()
        {
            if (Input.GetKey(Controls.CameraRotateKey))
            {
                Quaternion CamTurnDir = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * CamRotationSpeed, Vector3.up);
                offset = CamTurnDir * offset;
            }

        }

        void CameraCollisionHandler()
        {
            Vector3 RayDirection =  transform.position - target.position;
            float Distance = Vector3.Distance(transform.position, target.position);
            if(Physics.Raycast(target.position, RayDirection.normalized, out RaycastHit hitInfo, Distance, RayHittableLayers))
            {
                if(hitInfo.collider != null)
                {
                    transform.position = hitInfo.point;
                }
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cam.Controls
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        Transform target;
        [SerializeField] Vector3 offset;
        [SerializeField] float FollowSpeed = 0.2f;
        [SerializeField] bool LookAtPlayer;
        

        private void Awake()
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        private void FixedUpdate()
        {
            FollowPlayerPosition();
        }

        void FollowPlayerPosition()
        {
            if(LookAtPlayer) transform.LookAt(target);
            transform.position = Vector3.Lerp(transform.position, target.position + offset, FollowSpeed * Time.deltaTime);
        }
    }
}


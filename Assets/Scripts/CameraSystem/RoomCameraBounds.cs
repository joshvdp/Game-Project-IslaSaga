using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CameraSystem
{
    public class RoomCameraBounds : MonoBehaviour
    {
        [SerializeField] private Vector3 boundary = Vector3.one;
        [SerializeField] private Vector3 boundOffset = Vector3.one;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private Mesh drawMesh;
        [SerializeField] private Transform player, follower;

        private float _sqrMagnitude;
        private Vector2 _maxBound, _minBound;
        
        private void Start() => SetBounds();

        private void Update()
        {
            // Only recalculate when boundary has been changed
            if (!Mathf.Approximately(_sqrMagnitude, boundary.sqrMagnitude))
                SetBounds();

            BoundFollower();
        }

        private void BoundFollower()
        {
            if (!follower || !player)
                return;
            
            follower.position = player.position;
            Vector3 processedBounds = follower.localPosition;
            processedBounds.y = 0;

            processedBounds.x = Mathf.Clamp(processedBounds.x, _minBound.x, _maxBound.x);
            processedBounds.z = Mathf.Clamp(processedBounds.z, _minBound.y, _maxBound.y);

            follower.localPosition = new Vector3(processedBounds.x, 0, processedBounds.z);
        }

        private void SetBounds()
        {
            _maxBound = new Vector2(boundary.x, boundary.z) * 0.5f;
            _minBound = _maxBound * -1;
            _sqrMagnitude = boundary.sqrMagnitude;
        }

#if UNITY_EDITOR
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(boundary.x, 0, boundary.z));
            
            if (drawMesh && follower)
                Gizmos.DrawMesh(drawMesh, follower.position, follower.rotation);
        }

        private void OnDrawGizmosSelected() => BoundFollower();

        private void OnValidate()
        {
            if (boxCollider != null || transform.TryGetComponent(out boxCollider))
            {
                boxCollider.size = boundary + boundOffset;
                boxCollider.center = new Vector3(0, boundary.y * .5f, 0);
            }
        }

#endif
        
    }
}

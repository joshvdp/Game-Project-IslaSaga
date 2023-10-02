using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controls;
public class CameraControllerNEW : MonoBehaviour
{
    [SerializeField] ControlBindings Controls;

    [SerializeField] float MaxZoomIn;
    [SerializeField] float MaxZoomOut;
    [SerializeField] LayerMask RayHittableLayers;

    [SerializeField] private float _mouseSensitivity = 3.0f;

    private float _rotationY;
    private float _rotationX;

    [SerializeField] private Transform target;

    [SerializeField] private float DistanceFromTarget = 3.0f;
    [SerializeField] float SmoothFollow;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] private float _smoothTime = 0.2f;

    [SerializeField] private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    [SerializeField] Vector3 OffSet;

    [SerializeField] float CameraAngle;

    public float CameraMoveSensitivityMobile = 0.2f;
    [HideInInspector] public FixedTouchField TouchField;
    PlayerInputs PlayerInput => FindObjectOfType<PlayerInputs>();
    private void Start()
    {
        if(PlayerInput.PlatformType == PlatformType.PC) CameraRotation();
    }
    void Update()
    {
        if (PlayerInput.PlatformType == PlatformType.PC)
        {
            transform.position = Vector3.Lerp(transform.position, target.position - transform.forward * DistanceFromTarget, 0.5f); // Set to 0.5f to prevent camera stutter.
            if (Input.GetKey(Controls.CameraRotateKey)) CameraRotation();
            CameraZoomInAndOut();
        }
        else if (PlayerInput.PlatformType == PlatformType.Mobile)
        {
            Debug.Log("PLATFORM IS MOBILE");
            MobileCameraControls();
        }

        CameraCollisionHandler();
    }
    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationY += mouseX;
        _rotationX += mouseY;

        // Apply clamping for x rotation 
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        // Apply damping between rotation changes
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target
    }
    void CameraCollisionHandler()
    {
        Vector3 RayDirection = transform.position - target.position;
        float Distance = Vector3.Distance(transform.position, target.position);
        if (Physics.Raycast(target.position, RayDirection.normalized, out RaycastHit hitInfo, Distance, RayHittableLayers))
        {
            if (hitInfo.collider != null) transform.position = hitInfo.point;
        }
    }
    void CameraZoomInAndOut() => DistanceFromTarget = Mathf.Clamp(DistanceFromTarget - Input.mouseScrollDelta.y, MaxZoomIn, MaxZoomOut);

    void MobileCameraControls()
    {
        Debug.Log(TouchField);
        if (TouchField == null) return;
        CameraAngle += TouchField.TouchDist.x * CameraMoveSensitivityMobile;
        transform.position = target.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * OffSet;
        transform.rotation = Quaternion.LookRotation(target.position + Vector3.up * 2f - transform.position, Vector3.up);
    }
}

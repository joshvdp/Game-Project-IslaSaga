using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controls;
using Manager;
public class CameraControllerNEW : MonoBehaviour
{
    [SerializeField] ControlBindings Controls;

    [SerializeField] float MaxZoomIn;
    [SerializeField] float MaxZoomOut;
    [SerializeField] LayerMask RayHittableLayers;


    private float _rotationY;
    private float _rotationX;

    [SerializeField] private Transform target;

    [SerializeField] private float DistanceFromTarget = 3.0f;
    [SerializeField] float SmoothFollow;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    [SerializeField] Vector3 OffSet;

    [SerializeField] float CameraAngle;

    Vector3 currentTouchDist;
    Vector3 oldTouchDist;
    public float BaseCameraRotateValue = 0.2f;
    [HideInInspector] public FixedTouchField TouchField;
    private void Start()
    {
        if(MainManager.Instance.Settings.PlatformType == PlatformType.PC) CameraRotation();
    }
    void Update()
    {
        if (MainManager.Instance.IsPaused()) return;
        if (MainManager.Instance.Settings.PlatformType == PlatformType.PC)
        {

            transform.position = target.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * OffSet;
            transform.rotation = Quaternion.LookRotation(target.position + Vector3.up * 2f - transform.position, Vector3.up);
            if (Input.GetKey(Controls.CameraRotateKey)) CameraRotation();
            else
            {
                currentTouchDist = new Vector3(Input.mousePosition.x, Input.mousePosition.y) - oldTouchDist;
                oldTouchDist = Vector3.zero;
                _rotationY = 0f;
            }
            //CameraZoomInAndOut();
        }
        else if (MainManager.Instance.Settings.PlatformType == PlatformType.Mobile)
        {
            MobileCameraControls();
        }
        CameraCollisionHandler();
    }
    void CameraRotation()
    {
        currentTouchDist = Input.mousePosition - oldTouchDist;
        oldTouchDist = Input.mousePosition;

        if (!SettingsHandler.Instance) return;
        float rotationSpeedClampValue = SettingsHandler.Instance.settingsData.LookSensitivityValue * 100f;
        _rotationY = Mathf.Clamp(_rotationY + currentTouchDist.normalized.x * 30f, -rotationSpeedClampValue, rotationSpeedClampValue);

        CameraAngle += _rotationY * BaseCameraRotateValue * SettingsHandler.Instance.settingsData.LookSensitivityValue;
        transform.position = target.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * OffSet;
        transform.rotation = Quaternion.LookRotation((target.position + Vector3.up * 2f - transform.position) * Time.deltaTime, Vector3.up);

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
        if (TouchField == null) return;
        CameraAngle += TouchField.TouchDist.x * BaseCameraRotateValue * SettingsHandler.Instance.settingsData.LookSensitivityValue;
        transform.position = target.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * OffSet;
        transform.rotation = Quaternion.LookRotation(target.position + Vector3.up * 2f - transform.position, Vector3.up);
    }
}

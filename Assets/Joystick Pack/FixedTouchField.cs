using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action OnTouchDown;
    public Action OnTouchUp;
    [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;
    private void OnEnable()
    {
        GlobalEvents.Instance.FindEvent("On Change Platform Type Mobile")?.AddListener(EnableRaycastTargetOnImage);
        GlobalEvents.Instance.FindEvent("On Change Platform Type PC")?.AddListener(DisableRaycastTargetOnImage);
    }

    private void OnDisable()
    {
        GlobalEvents.Instance.FindEvent("On Change Platform Type PC")?.AddListener(DisableRaycastTargetOnImage);
        GlobalEvents.Instance.FindEvent("On Change Platform Type Mobile")?.RemoveListener(EnableRaycastTargetOnImage);
    }
    void DisableRaycastTargetOnImage() => GetComponent<Image>().raycastTarget = false;
    void EnableRaycastTargetOnImage() => GetComponent<Image>().raycastTarget = true;
    // Update is called once per frame
    void Update()
    {
        if (Pressed)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = new Vector2();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouchDown?.Invoke();
        Pressed = true;
        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        OnTouchUp?.Invoke();
        Pressed = false;
    }

}
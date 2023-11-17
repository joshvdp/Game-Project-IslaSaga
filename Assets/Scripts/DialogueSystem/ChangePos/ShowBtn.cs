using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBtn : MonoBehaviour
{
    public GameObject btn, firstDia;

    public FixedTouchField TouchField;


    RectTransform bg;

    private void Start()
    {
        bg = gameObject.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        TouchField.OnTouchDown += DoThisOnDown;
    }

    private void OnDisable()
    {
        TouchField.OnTouchDown -= DoThisOnDown;
    }

    void DoThisOnDown()
    {
        if (firstDia.activeSelf)
        {
            bg.transform.localPosition = new Vector3(172, 422, 0f);
            btn.SetActive(true);
        }

    }
}

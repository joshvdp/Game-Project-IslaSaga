using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finished : MonoBehaviour
{
    public FixedTouchField TouchField;

    public GameObject   lastDia,
                        buttons, pause;  // buttons & UI

    RectTransform bg;

    private void Start()
    {
        bg = gameObject.GetComponent<RectTransform>();
        pause.SetActive(false);
        buttons.SetActive(false);
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
        if (lastDia.activeSelf)
        {
            pause.SetActive(true);
            buttons.SetActive(true);
        }
    }
}

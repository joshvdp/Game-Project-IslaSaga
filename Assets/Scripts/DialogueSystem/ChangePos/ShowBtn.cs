using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBtn : MonoBehaviour
{
    public FixedTouchField TouchField;

    public GameObject btn, firstDia, lastDia,
                      analog, atk, sprint, block, jump, pause;


    RectTransform bg;

    private void Start()
    {
        bg = gameObject.GetComponent<RectTransform>();
        pause.SetActive(false);
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
            bg.transform.localPosition = new Vector3(334, 487, 0f);
        }

        if (lastDia.activeSelf)
        {
            bg.transform.localPosition = new Vector3(172, 422, 0f);
            atk.SetActive(false);
            sprint.SetActive(false);
            block.SetActive(false);
            jump.SetActive(false);
            analog.SetActive(false);
            btn.SetActive(true);
        }

    }
}

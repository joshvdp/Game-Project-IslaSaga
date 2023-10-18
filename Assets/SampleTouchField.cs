using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SampleTouchField : MonoBehaviour
{
    public FixedTouchField TouchField; // Assign mo ung touch field sa inspector na to, or assign mo through code.

    private void OnEnable()
    {
        TouchField.OnTouchDown += DoThisOnDown;
        TouchField.OnTouchUp += DoThisOnUp;
    }
    private void OnDisable()
    {
        TouchField.OnTouchDown -= DoThisOnDown;
        TouchField.OnTouchUp -= DoThisOnUp;
    }

    void DoThisOnDown()
    {
        Debug.Log("TOUCH DOWN");
    }

    void DoThisOnUp()
    {
        Debug.Log("TOUCH UP");
    }
}

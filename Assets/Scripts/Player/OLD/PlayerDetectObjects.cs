using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectObjects : MonoBehaviour
{
    public GameObject MoveableObjectInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Moveable")
        {
            MoveableObjectInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Moveable" && other.gameObject == MoveableObjectInRange)
        {
            MoveableObjectInRange = null;
        }
    }
}

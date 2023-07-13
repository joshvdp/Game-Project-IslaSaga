using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateCS : MonoBehaviour
{
    Vector3 StartingPos;
    Vector3 MaxDistancePosition;
    [Header("References")]
    [SerializeField] PressurePlateGateCS MasterGate;
    [SerializeField] Renderer ObjectRenderer;
    [Header("Variables")]
    [SerializeField] float PressureSpeed;
    [SerializeField] int ObjectsOnTop = 0;
    public bool IsPressed;
    void Start()
    {
        StartingPos = transform.position;
        MaxDistancePosition = new Vector3(StartingPos.x, StartingPos.y - 0.20f, StartingPos.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Moveable")
        {
            ObjectsOnTop++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Moveable")
        {
            ObjectsOnTop--;
        }
    }

    private void Update()
    {
        if(ObjectsOnTop>0)
        {
            ObjectRenderer.material.color = Color.red;
            GoDown();
        }

        if (ObjectsOnTop <= 0)
        {
            ObjectRenderer.material.color = Color.green;
            GoUp();
        }

        if(transform.position.y <= MaxDistancePosition.y && !IsPressed)
        {
            IsPressed = true;
        }

        if (transform.position.y >= StartingPos.y && IsPressed)
        {
            IsPressed = false;
        }

    }

    void GoDown()
    {
        if (transform.position.y >= MaxDistancePosition.y)
            transform.Translate(Vector3.down * Time.deltaTime * PressureSpeed, Space.World);
    }

    void GoUp()
    {
        if(transform.position.y <= StartingPos.y)
        transform.Translate(Vector3.up * Time.deltaTime * PressureSpeed, Space.World);
    }
}

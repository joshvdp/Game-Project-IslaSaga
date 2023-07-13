using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateGateCS : MonoBehaviour
{
    Vector3 StartingPosition;

    [Header("Pressure Plates Connected")]
    public PressurePlateCS[] ConnectedPressurePlates;

    public bool AllPlatesPressed = false;

    [Header("For Disappearing")]
    [SerializeField] Renderer GateRenderer;
    [SerializeField] Collider GateCollider;

    [Header("For Opening")]
    [SerializeField] float OpenSpeed;
    [SerializeField] Vector3 OpenPosition;

    [Header("Choose 1 for Opening")]
    [SerializeField] bool MustOpen;
    [SerializeField] bool MustDisappear;

    private void Awake()
    {
        StartingPosition = transform.position;
    }
    private void Update()
    {
        
        GateRenderer.material.color =  AllPlatesPressed ? Color.green : Color.red;
       
        CheckPressedPressurePlates();
    }

    void DoAction()
    {
        if(MustOpen)
        {
            GoToStartingPosition();
        }

        if(MustDisappear)
        {
            Disappear();
        }
    }

    public void CheckPressedPressurePlates()
    {
        int PressedPressurePlates = 0;
        for (int i = 0; i < ConnectedPressurePlates.Length; i++)
        {
            if (ConnectedPressurePlates[i].IsPressed == true)
            {
                PressedPressurePlates++;
            }
        }
        if(PressedPressurePlates >= ConnectedPressurePlates.Length)
        {
            AllPlatesPressed = true;
            DoAction();
        }
        else
        {
            Appear();
            GoToOpenPosition();
        }
    }

    void GoToOpenPosition()
    {
        if (Vector3.Distance(transform.position, StartingPosition) > 0)
        {
            transform.position += (StartingPosition - transform.position).normalized * OpenSpeed * Time.deltaTime;
        }
    }

    void GoToStartingPosition()
    {
        if (Vector3.Distance(transform.position, OpenPosition) > 0)
        {
            transform.position += (OpenPosition - transform.position).normalized * OpenSpeed * Time.deltaTime;
        }
    }

    void Disappear()
    {
        GateRenderer.enabled = false;
        GateCollider.enabled = false;
    }

    void Appear()
    {
        AllPlatesPressed = false;
        GateRenderer.enabled = true;
        GateCollider.enabled = true;
    }

}

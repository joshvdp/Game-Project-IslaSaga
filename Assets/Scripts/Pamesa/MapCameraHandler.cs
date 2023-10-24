using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraHandler : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}

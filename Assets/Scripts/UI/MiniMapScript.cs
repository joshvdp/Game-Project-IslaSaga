using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] float Height;

    void LateUpdate()
    {
        Vector3 newPosition = Player.position + (Vector3.up * Height);
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        
        transform.rotation = Quaternion.Euler(90f, Camera.main.transform.eulerAngles.y, 0f);
    }
}

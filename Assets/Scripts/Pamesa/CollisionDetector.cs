using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    public GameObject dialogueBox;

    private void Start()
    {
        dialogueBox.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Moveable Box")
        {
            dialogueBox.SetActive(true);
            StartCoroutine(gameObject.GetComponent<Dialogue>().TypeLine());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{

    public int dialogueIndex;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Moveable Box")
        {
            DialogueHandler.Instance.EnableDialogue(dialogueIndex);
        }
    }
}

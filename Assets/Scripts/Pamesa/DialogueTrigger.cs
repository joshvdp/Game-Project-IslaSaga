using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {

        public int dialogueIndex;
        public int controlIndex;
        [SerializeField] private GameObject dialogueCollider;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Moveable Box")
            {
                DialogueHandler.Instance.EnableDialogue(dialogueIndex);
            }

            if (collision.collider.name == "Dialogue Collider")
            {
                DialogueHandler.Instance.EnableDialogue(controlIndex);
                dialogueCollider.SetActive(false);
            }
        }
    }
}


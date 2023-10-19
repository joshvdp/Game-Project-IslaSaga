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
        public float sec = 3;
        [SerializeField] private GameObject dialogueCollider;

        private void Start()
        {
            dialogueCollider.SetActive(false);
            StartCoroutine(WaitForSeconds());
        }

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

        private IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(sec);
            dialogueCollider.SetActive(true);
        }
    }
}


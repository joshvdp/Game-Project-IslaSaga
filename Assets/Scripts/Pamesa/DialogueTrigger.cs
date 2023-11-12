using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DialogueSystem
{
    public enum UIPlatformType
    {
        PC,
        Mobile
    }
    public class DialogueTrigger : MonoBehaviour
    {
        public UIPlatformType PlatformType;

        
        int index;
        [SerializeField] private GameObject dialogueCollider;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.name == "Moveable Box")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 0;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }
            }

            if (collision.collider.name == "Dialogue Collider")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 1;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 2;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }
                
                dialogueCollider.SetActive(false);
            }
        }
    }
}


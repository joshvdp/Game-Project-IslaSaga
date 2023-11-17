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
        [SerializeField] private GameObject dialogueCollider, Map_HP, Potions, Weapons, Inventory_Button;
        

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

            

            if (collision.collider.name == "Dialogue Collider (Map_HP)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 1;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 3;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                dialogueCollider.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Potions)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 1;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 4;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                dialogueCollider.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Weapons)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 1;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 5;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                dialogueCollider.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Inventory Button)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 1;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 6;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                dialogueCollider.SetActive(false);
            }
        }
    }
}


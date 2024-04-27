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

        public GameObject[] colliderArray;

        private bool allMarksVisited = false;

        private bool keyPressed = false;


        int index;
        [SerializeField] private GameObject Map_HP, Potions, Weapons, Inventory_Button, Start, Low_Health, Halfway, Vases, Cannons, Puzzle, End, IsFinished, NPC_Shop, SwordChest,BootsChest;

        


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
                
                //dialogueCollider.SetActive(false);
            }

            

            if (collision.collider.name == "Dialogue Collider (Map_HP)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 22;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 3;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }
                

                Map_HP.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Potions)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 18;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 4;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                Potions.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Weapons)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 23;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 5;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                

                Weapons.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Inventory Button)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 19;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 6;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                Inventory_Button.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Start)")
            {

                index = 8;
                DialogueHandler.Instance.EnableDialogue(index);

                Start.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Low_Health)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 25;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 9;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }
                

                Low_Health.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Halfway)")
            {
               
                index = 10;
                DialogueHandler.Instance.EnableDialogue(index);

                Halfway.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Vases)")
            {

                index = 11;
                DialogueHandler.Instance.EnableDialogue(index);

                Vases.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Cannons)")
            {

                index = 12;
                DialogueHandler.Instance.EnableDialogue(index);

                Cannons.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (Puzzle)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 20;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 13;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }

                Puzzle.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (End)")
            {
                switch (PlatformType)
                {
                    case UIPlatformType.PC:
                        index = 26;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                    case UIPlatformType.Mobile:
                        index = 14;
                        DialogueHandler.Instance.EnableDialogue(index);
                        break;
                }
               

                End.SetActive(false);
            }

            if (collision.collider.name == "Dialogue Collider (IsFinished)")
            {
                if (!allMarksVisited)
                {
                    switch (PlatformType)
                    {
                        case UIPlatformType.PC:
                            index = 24;
                            DialogueHandler.Instance.EnableDialogue(index);
                            break;
                        case UIPlatformType.Mobile:
                            index = 15;
                            DialogueHandler.Instance.EnableDialogue(index);
                            break;
                    }

                }
                else
                {
                    IsFinished.SetActive(false);
                }
            }

            if (collision.collider.name == "Dialogue Collider (Shop)")
            {

                index = 21;
                DialogueHandler.Instance.EnableDialogue(index);

            }

            if (collision.collider.name == "Treasure Chest (1)")
            {
                if (keyPressed)
                {
                    StartCoroutine(Sword());
                }
                

                
            }

            

        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.collider.name == "Treasure Chest (Ability Double Jump)" && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Collided with the treasure chest and pressed 'E' key.");
                StartCoroutine(Boots());

            }

            if (collision.collider.name == "Treasure Chest (1)" && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Collided with the treasure chest and pressed 'E' key.");
                StartCoroutine(Sword());

            }
        }


        void Sword_Chests()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Sword());
            }
            
        }

        void Boots_Chest()
        {
            if (keyPressed == true)
            {
                StartCoroutine(Boots());
            }

        }

        IEnumerator Sword()
        {
            yield return new WaitForSeconds(2);
            index = 27;
            DialogueHandler.Instance.EnableDialogue(index);
        }

        IEnumerator Boots()
        {
            yield return new WaitForSeconds(2);
            index = 28;
            DialogueHandler.Instance.EnableDialogue(index);
        }

        private void Update()
        {

            


            if (!allMarksVisited)
            {
                allMarksVisited = CheckAllMarksVisited();
            }
        }
        bool CheckAllMarksVisited()
        {
            foreach (GameObject collider in colliderArray)
            {
                if (collider.activeSelf)
                {
                    return false;
                }
            }
            return true;
        }

    }
}


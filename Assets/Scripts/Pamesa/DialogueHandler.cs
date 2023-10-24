using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHandler : MonoBehaviour
    {
        public static DialogueHandler Instance;

        public List<GameObject> dialogues;
        public GameObject ActiveDialogue { get; private set; }
        public GameObject pauseButton, map, health;

        private void Awake() => Instance = this;

        public void EnableDialogue(int dialogueIndex)
        {
            DisableActiveDialogue();

            dialogues[dialogueIndex].SetActive(true);
            ActiveDialogue = dialogues[dialogueIndex];

            //pauseButton.SetActive(false);

        }

        public void DisableActiveDialogue()
        {

            if (ActiveDialogue == null)
                return;

            ActiveDialogue.SetActive(false);
            //pauseButton.SetActive(true);


        }
    }
}


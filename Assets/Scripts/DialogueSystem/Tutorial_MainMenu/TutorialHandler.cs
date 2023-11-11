using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class TutorialHandler : MonoBehaviour
    {
        public static TutorialHandler Instance;

        public List<GameObject> dialogues;
        public GameObject ActiveDialogue { get; private set; }


        private void Awake() => Instance = this;

        public void EnableDialogue(int tutorialIndex)
        {
            DisableActiveDialogue();

            dialogues[tutorialIndex].SetActive(true);
            ActiveDialogue = dialogues[tutorialIndex];
        }

        public void DisableActiveDialogue()
        {
            if (ActiveDialogue == null)
                return;

            ActiveDialogue.SetActive(false);
        }
    }
}


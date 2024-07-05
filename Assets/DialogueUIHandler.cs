using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


namespace Core
{
    public class DialogueUIHandler : MonoBehaviour
    {
        public static DialogueUIHandler Instance;

        [SerializeField] DialoguePlayer DialoguePlayer;
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        public UnityEvent OnDialogueStart;
        public UnityEvent OnDialogueEnd;

        public void StartDialogue(DialogueSCO dialogue, float delay)
        {
            DialoguePlayer.Dialogue = dialogue;
            DialoguePlayer.gameObject.SetActive(true);
            OnDialogueStart?.Invoke();
        }

        public void EndDialogue()
        {
            DialoguePlayer.gameObject.SetActive(false);
            OnDialogueEnd?.Invoke();
        }

    }

}

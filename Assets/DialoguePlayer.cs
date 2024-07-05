using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueSystem;
namespace Core
{
    public class DialoguePlayer : MonoBehaviour
    {
        public DialogueSCO Dialogue;

        [SerializeField] FixedTouchField TouchField;
        [SerializeField] TextMeshProUGUI NameText;
        [SerializeField] TextMeshProUGUI DialogueText;

        int CurrentDialogueIndex = 0;

        IEnumerator WriteLineIEnumerator;
        public bool FinishedWriting { get; protected set; }
        private void OnEnable()
        {
            TouchField.OnTouchDown += CheckLineStatus;
            StartDialogueLine(0);
            TouchField.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            TouchField.OnTouchDown -= CheckLineStatus;
            FinishDialogue();
            TouchField.gameObject.SetActive(false);
        }


        private void Update()
        {

        }


        void StartDialogueLine(int dialogueIndex)
        {
            FinishedWriting = false;
            EmptyTexts();
            NameText.text = Dialogue.DialogueLines[dialogueIndex].Name;
            WriteLineIEnumerator = WriteText(Dialogue.DialogueLines[dialogueIndex].Dialogue, DialogueText, 0.1f);
            StartCoroutine(WriteLineIEnumerator);
        }

        void CheckLineStatus()
        {
            if(FinishedWriting)
            {
                if (CurrentDialogueIndex + 1 < Dialogue.DialogueLines.Length)
                {
                    StartDialogueLine(CurrentDialogueIndex++);
                    return;
                }
                else
                {
                    DialogueUIHandler.Instance.EndDialogue();
                    return;
                }
            }
            if (DialogueText.text != Dialogue.DialogueLines[CurrentDialogueIndex].Dialogue) FinishCurrentLine();
        }

        void FinishCurrentLine()
        {
            StopCoroutine(WriteLineIEnumerator);
            DialogueText.text = Dialogue.DialogueLines[CurrentDialogueIndex].Dialogue;
            FinishedWriting = true;
        }
        void EmptyTexts()
        {
            NameText.text = "";
            DialogueText.text = "";
        }
        void FinishDialogue()
        {
            Dialogue = null;
            CurrentDialogueIndex = 0;
            EmptyTexts();
        }

        protected IEnumerator WriteText(string input, TextMeshProUGUI textHolder, float delay)
        {
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSecondsRealtime(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButton(0));
            FinishedWriting = true;
        }
    }

}

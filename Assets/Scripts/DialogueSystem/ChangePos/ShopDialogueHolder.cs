using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class ShopDialogueHolder : MonoBehaviour
    {
        private IEnumerator dialogueSeq;

        private void OnEnable()
        {
            dialogueSeq = dialogueSequence();
            StartCoroutine(dialogueSeq);
        }
        private IEnumerator dialogueSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform childTransform = transform.GetChild(i);

                if (childTransform.parent == null || childTransform.parent != transform)
                    continue;

                Deactivate();

                childTransform.gameObject.SetActive(true);
                yield return new WaitUntil(() =>
                {
                    if (childTransform.TryGetComponent(out DialogueLine dialogueLine))
                        return dialogueLine.Finished;
                    
                    Debug.Log($"The {childTransform.name} has no Dialogue Line Component.");
                    return false;
                });
            }

            Time.timeScale = 1f;
            gameObject.SetActive(false);


        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform childTransform = transform.GetChild(i);

                if (childTransform.parent == null || childTransform.parent != transform)
                    continue;
                
                childTransform.gameObject.SetActive(false);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {

        public bool Finished { get; protected set; }
        protected IEnumerator WriteText(string input, TMP_Text textHolder, float delay)
        {
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButton(0));
            Finished = true;
        }
    }
}



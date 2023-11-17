using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class ShowFirst : MonoBehaviour
    {
        int index;
        private void Awake()
        {
            StartCoroutine(show());
        }

        IEnumerator show()
        {
            string activeSceneName = SceneManager.GetActiveScene().name;
            if (activeSceneName == "Tutorial Scene")
            {
                yield return new WaitForSeconds(2);
                index = 2;
                DialogueHandler.Instance.EnableDialogue(index);
            }
        }
    }
}



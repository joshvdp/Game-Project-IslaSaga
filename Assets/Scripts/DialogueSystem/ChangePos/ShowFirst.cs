using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Manager;

namespace DialogueSystem
{
    public class ShowFirst : MonoBehaviour
    {
        public float ShowDelay;
        int index;
        public GameObject inventoryBtn, inventoryBtnTutorial, analog, analogTutorial;
        private bool hasBeenCalled = false;
        private void Update()
        {
            if (!hasBeenCalled && !SceneLoader.Instance.IsLoadingScreenPresent)
            {
                StartCoroutine(show());
            }
            
        }

        IEnumerator show()
        {
            string activeSceneName = SceneManager.GetActiveScene().name;
            if (activeSceneName == "Tutorial Scene")
            {
                inventoryBtn.SetActive(false);
                analog.SetActive(false);
                yield return new WaitForSeconds(ShowDelay);
                index = 2;
                DialogueHandler.Instance.EnableDialogue(index);
            }
            else
            {
                analogTutorial.SetActive(false);
                inventoryBtn.SetActive(true);
                inventoryBtnTutorial.SetActive(false);
            }

            hasBeenCalled = true;
        }
    }
}



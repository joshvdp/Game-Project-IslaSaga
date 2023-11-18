using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class ShowFirst : MonoBehaviour
    {
        public float ShowDelay;
        int index;
        public GameObject inventoryBtn, inventoryBtnTutorial;
        private void Awake()
        {
            StartCoroutine(show());
        }

        IEnumerator show()
        {
            string activeSceneName = SceneManager.GetActiveScene().name;
            if (activeSceneName == "Tutorial Scene")
            {
                inventoryBtn.SetActive(false);
                yield return new WaitForSeconds(ShowDelay);
                index = 2;
                DialogueHandler.Instance.EnableDialogue(index);
            }
            else
            {
                inventoryBtn.SetActive(true);
                inventoryBtnTutorial.SetActive(false);
            }
        }
    }
}



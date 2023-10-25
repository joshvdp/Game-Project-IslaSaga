using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;

        public float transitionTime = 1f;


        

        public void LoadNextLevel(int sceneID)
        {
            StartCoroutine(LoadLevel(sceneID));
        }

        private IEnumerator LoadLevel(int sceneID)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneID);
        }
    }
}


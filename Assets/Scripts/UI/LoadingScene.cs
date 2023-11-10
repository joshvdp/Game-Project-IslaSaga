using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LoadScreen
{
    public class LoadingScene : MonoBehaviour
    {
        public GameObject LoadingScreen;

        public GameObject Logo;
        float spinSpeed = 0.1f;

        bool isLoading;
        
        //public Image LoadingBarFill;

        public void LoadScene(int sceneID)
        {
            StartCoroutine(LoadSceneAsync(sceneID));
        }

        private IEnumerator LoadSceneAsync(int sceneID)
        {
            isLoading = true;
            LoadingScreen.SetActive(true);
            StartCoroutine(spin());

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
            operation.allowSceneActivation = false;
            

            while (!operation.isDone)
            {
                yield return new WaitForSeconds(7);
                operation.allowSceneActivation = true;
            }

            

            LoadingScreen.SetActive(false);

            isLoading = false;

            /*while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

                LoadingBarFill.fillAmount = progressValue;

                yield return null;
            }*/
        }

        private IEnumerator spin()
        {
            while (isLoading)
            {
                Logo.transform.Rotate(0, -spinSpeed, 0);
                yield return null;
            }
        }
    }
}


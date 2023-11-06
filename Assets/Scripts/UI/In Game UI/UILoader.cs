using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class UILoader : MonoBehaviour
    {

        private void OnEnable()
        {
            SceneLoader.Instance.LoadingScreenLoaded += UnloadThisScene;
        }

        private void OnDisable()
        {
            SceneLoader.Instance.LoadingScreenLoaded -= UnloadThisScene;
        }
        public void Start()
        {
            loadUI();
        }
        public void loadUI()
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
        public void UnloadThisScene() => SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }
}

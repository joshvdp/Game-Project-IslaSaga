using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

namespace Manager
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;

        public Action LoadingScreenLoaded;
        public SceneNames SceneName;

        [Foldout("Next Scene Variables")] public Sprite BGOfLoadingScreen;
        [Foldout("Next Scene Variables")] public string NextSceneToLoad;
        [Foldout("Next Scene Variables")] public LightingSettings NextSceneLightingSettings;

        public bool IsLoadingScreenPresent => FindObjectOfType<LoadingSceneHandler>();

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }
        public void LoadNextSceneAsync(string SceneToLoad)
        {
            NextSceneToLoad = SceneToLoad;
            SceneManager.LoadScene("Loading Screen", LoadSceneMode.Additive);
        }

        public void UnloadThisScene()
        {
            LoadingScreenLoaded?.Invoke();
            Debug.Log(SceneManager.GetSceneByName("InGameUI").IsValid());
            if(SceneManager.GetSceneByName("InGameUI").IsValid()) SceneManager.UnloadSceneAsync("InGameUI");
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void exitGame()
        {
            Application.Quit();
        }
    }
}


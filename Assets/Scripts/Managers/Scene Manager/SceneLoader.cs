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
        public SceneNames SceneNames;

        [Foldout("Next Scene Variables")] public Sprite BGOfLoadingScreen;
        [Foldout("Next Scene Variables")] public string NextSceneToLoad;
        [Foldout("Next Scene Variables")] public LightingSettings NextSceneLightingSettings;
        [SerializeField] bool IsTutorialScene = false;
        public bool IsLoadingScreenPresent => FindObjectOfType<LoadingSceneHandler>();
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }
        private void Start()
        {
            if (!MainManager.Instance) return;
            if(!IsTutorialScene) LoadSceneAdditive(SceneNames.InGameUI); 
            else LoadSceneAdditive(SceneNames.TutorialInGameUI);
        }

        public void ReloadLevel()
        {
            if (LoadingSceneHandler.Instance) return;
            LoadNextSceneAsync(SceneManager.GetActiveScene().name);
            MainManager.Instance.SetTimeScale(1);
        }
        void LoadSceneAdditive(string SceneToLoad)
        {
            if (SceneManager.GetSceneByName(SceneToLoad).IsValid()) return;
            SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Additive);
        }
        public void LoadNextSceneAsync(string SceneToLoad)
        {
            if (LoadingSceneHandler.Instance) return;
            NextSceneToLoad = SceneToLoad;
            SceneManager.LoadScene("Loading Screen", LoadSceneMode.Additive);
        }

        public void UnloadThisScene()
        {
            LoadingScreenLoaded?.Invoke();
            if (SceneManager.GetSceneByName(SceneNames.InGameUI).IsValid()) SceneManager.UnloadSceneAsync(SceneNames.InGameUI);
            if (SceneManager.GetSceneByName(SceneNames.TutorialInGameUI).IsValid()) SceneManager.UnloadSceneAsync(SceneNames.TutorialInGameUI);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}


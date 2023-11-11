using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class SceneLoader : MonoBehaviour
{
    public Action LoadingScreenLoaded;

    public static SceneLoader Instance;


    [SerializeField, Foldout("Next Scene Variables")] public Texture BGOfLoadingScreen;
    [SerializeField, Foldout("Next Scene Variables")] public string NextSceneToLoad;
    [SerializeField, Foldout("Next Scene Variables")] public LightingSettings NextSceneLightingSettings;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void LoadNextSceneAsync()
    {
        SceneManager.LoadScene("Loading Screen", LoadSceneMode.Additive);

    }

    public void UnloadThisScene()
    {
        LoadingScreenLoaded?.Invoke();
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.UnloadSceneAsync("InGameUI");
    }
        
        

}

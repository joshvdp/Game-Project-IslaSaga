using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Action LoadingScreenLoaded;

    public static SceneLoader Instance;

    public Texture BGOfLoadingScreen;
    public string NextSceneToLoad;
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
    }
        
        

}

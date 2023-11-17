using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Manager;
using UnityEditor;
using NaughtyAttributes;
using UnityEngine.Events;

public class LoadingSceneHandler : MonoBehaviour
{
    [SerializeField, Foldout("Referencing")] public Animator CanvasAnimator;
    [SerializeField, Foldout("Referencing")] public RawImage BackgroundVariable;
    [SerializeField, Foldout("Referencing")] public Slider LoadingBar;

    [SerializeField, Foldout("Next Scene Variables")] public string SceneToLoad;
    [SerializeField, Foldout("Next Scene Variables")] public Texture BackgroundImage;

    public static LoadingSceneHandler Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        Debug.Log("LOADING SCENE LOADED");
        BackgroundImage = SceneLoader.Instance?.BGOfLoadingScreen;

        SceneToLoad = SceneLoader.Instance? SceneLoader.Instance?.NextSceneToLoad : SceneToLoad ;

        BackgroundVariable.color = BackgroundImage != null? Color.white : Color.black ;
        if (BackgroundImage != null) BackgroundVariable.texture = BackgroundImage;
    }
    public void LoadNextSceneAsync()
    {
        SceneLoader.Instance?.UnloadThisScene();
        StartCoroutine(LoadSceneAsync(SceneToLoad));
    }
    public void UnloadThisScene() => SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Loading Screen").buildIndex);

    IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float ProgressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBar.value = ProgressValue;
            yield return null;
        }
        Time.timeScale = 1;
        CanvasAnimator.SetTrigger("Fade Out");
    }
}

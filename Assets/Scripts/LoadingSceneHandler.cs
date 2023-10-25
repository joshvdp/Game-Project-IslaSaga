using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Manager;
public class LoadingSceneHandler : MonoBehaviour
{
    public string SceneToLoad;
    public Texture BackgroundImage;


    public Animator CanvasAnimator;
    public RawImage BackgroundVariable;
    public Slider LoadingBar;
    private void Start()
    {
        

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

        CanvasAnimator.SetTrigger("Fade Out");
    }
}

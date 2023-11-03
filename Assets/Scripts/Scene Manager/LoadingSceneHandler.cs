using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Manager;
using UnityEditor;
using NaughtyAttributes;
public class LoadingSceneHandler : MonoBehaviour
{
    [SerializeField, Foldout("Referencing")] public Animator CanvasAnimator;
    [SerializeField, Foldout("Referencing")] public RawImage BackgroundVariable;
    [SerializeField, Foldout("Referencing")] public Slider LoadingBar;

    [SerializeField, Foldout("Next Scene Variables")] public string SceneToLoad;
    [SerializeField, Foldout("Next Scene Variables")] public Texture BackgroundImage;
    [SerializeField, Foldout("Next Scene Variables")] public LightingSettings LightingSettingOfNextScene;

   
    private void Start()
    {
        LightingSettingOfNextScene = SceneLoader.Instance?.NextSceneLightingSettings;
        BackgroundImage = SceneLoader.Instance?.BGOfLoadingScreen;

        SceneToLoad = SceneLoader.Instance? SceneLoader.Instance?.NextSceneToLoad : SceneToLoad ;

        BackgroundVariable.color = BackgroundImage != null? Color.white : Color.black ;
        if (BackgroundImage != null) BackgroundVariable.texture = BackgroundImage;
    }
    public void LoadNextSceneAsync()
    {
        SceneLoader.Instance?.UnloadThisScene();
        StartCoroutine(LoadSceneAsync(SceneToLoad));
        if (LightingSettingOfNextScene != null) Lightmapping.lightingSettings = LightingSettingOfNextScene;
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

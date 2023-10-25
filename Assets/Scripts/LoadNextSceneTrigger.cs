using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
[RequireComponent(typeof(Collider))]
public class LoadNextSceneTrigger : MonoBehaviour
{
    public Texture LoadingScreenBG;
    public string NextScene;

    private void OnTriggerEnter(Collider other)
    {
        SceneLoader.Instance.BGOfLoadingScreen = LoadingScreenBG;
        SceneLoader.Instance.NextSceneToLoad = NextScene;
        SceneLoader.Instance?.LoadNextSceneAsync();
    }
}

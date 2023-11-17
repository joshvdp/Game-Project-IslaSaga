using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using NaughtyAttributes;
[RequireComponent(typeof(Collider))]
public class LoadNextSceneTrigger : MonoBehaviour
{
    [SerializeField, Foldout("Next Scene Variables")] public Sprite LoadingScreenBG;
    [SerializeField, Foldout("Next Scene Variables")] public string NextSceneName;
    private void OnTriggerEnter(Collider other)
    {
        SceneLoader.Instance.BGOfLoadingScreen = LoadingScreenBG;
        SceneLoader.Instance?.LoadNextSceneAsync(NextSceneName);
    }
}

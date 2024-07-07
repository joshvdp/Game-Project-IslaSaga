using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using NaughtyAttributes;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class LoadNextSceneTrigger : MonoBehaviour
{
    public UnityEvent OnLoadNextSceneTriggered;
    [SerializeField] bool ShouldSave;
    [SerializeField] bool ShouldDestroyOnEnter;
    [SerializeField, Foldout("Next Scene Variables")] public Sprite LoadingScreenBG;
    [SerializeField, Foldout("Next Scene Variables")] public string NextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldSave) SaveSystemJSON.Instance.SaveData();
        OnLoadNextSceneTriggered?.Invoke();
        SceneLoader.Instance.BGOfLoadingScreen = LoadingScreenBG;
        SceneLoader.Instance?.LoadNextSceneAsync(NextSceneName);
        if (ShouldDestroyOnEnter) Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class MenuHandler : MonoBehaviour
{
    public void InvokeDeleteSaveData()
    {
        SaveSystemJSON.Instance.DeleteSaveData();
    }
    public void InvokeLoadNextSceneAsync(string sceneName)
    {
        SceneLoader.Instance.LoadNextSceneAsync(sceneName);
    }
}

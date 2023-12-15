using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ManagersEventAndFunctionsHolder : MonoBehaviour
    {
        // This script prevents missing reference of manager gameobject when it is in a unityevent

        #region Main Manager Events and Functions
        public void InvokeStartBossFight() => MainManager.Instance.StartBossFight();
        public void InvokeEndBossFight() => MainManager.Instance.EndBossFight();
        public void InvokeResetPlayerStats() => MainManager.Instance.ResetPlayerStats();
        #endregion
        #region Scene Loader Events and Functions
        public void InvokeLoadNextScene(string sceneToLoad) => SceneLoader.Instance.LoadNextSceneAsync(sceneToLoad);
        public void InvokeUnloadThisScene() => SceneLoader.Instance.UnloadThisScene();
        #endregion

        #region Save System Events and Functions

        public void InvokeDeleteSaveData() => SaveSystemJSON.Instance.DeleteSaveData();
        public void InvokeSaveData() => SaveSystemJSON.Instance.SaveData();
        public void InvokeReadSaveData(string path) => SaveSystemJSON.Instance.ReadJson(path);

        #endregion


        #region 

        #endregion

        #region 

        #endregion

    }
}

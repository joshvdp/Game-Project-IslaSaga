using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScenes
{
    public class GlobalSFX : MonoBehaviour
    {
        public void Start()
        {
            loadUI();
        }
        public void loadUI()
        {
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            Debug.Log("Load UI");
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class ScenesManager : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        public void exit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}


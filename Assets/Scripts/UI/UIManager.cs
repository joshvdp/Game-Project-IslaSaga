using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.SceneManagement;
using System;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public List<Screen> Screens;
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }
        private void OnEnable()
        {
            MainManager.Instance?.PlayerMachine?.HpComponent.onDeath.AddListener(GameOver);
        }
        private void OnDisable()
        {
            MainManager.Instance?.PlayerMachine?.HpComponent.onDeath.RemoveListener(GameOver);

        }
        public void ToggleScreen(string ScreenName) => Screens.Find(_ => _.ScreenName == ScreenName)?.ScreenObject.SetActive(!Screens.Find(_ => _.ScreenName == ScreenName).ScreenObject.activeSelf);
        public void GameOver()
        {
            Screens.Find(_ => _.ScreenName == "Game Over Screen")?.ScreenObject.SetActive(true);
            foreach (Screen screenRef in Screens)
            {
                Screens.Find(_ => _.ScreenName != "Game Over Screen")?.ScreenObject.SetActive(false);
                Debug.Log("SET TO FALSE");
            }
        }

            public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    [Serializable]
    public class Screen
    {
        public GameObject ScreenObject;
        public string ScreenName;
        
    }
}
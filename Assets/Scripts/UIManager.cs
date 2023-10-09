using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public GameObject GameOverScreen;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }
        private void OnEnable()
        {
            if (FindObjectOfType<PlayerHpHandler>()) FindObjectOfType<PlayerHpHandler>().onPlayerDeath += GameOver;
        }
        private void OnDisable()
        {
            if(FindObjectOfType<PlayerHpHandler>()) FindObjectOfType<PlayerHpHandler>().onPlayerDeath -= GameOver;
        }

        public void GameOver()
        {
            GameOverScreen.SetActive(true);
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
}
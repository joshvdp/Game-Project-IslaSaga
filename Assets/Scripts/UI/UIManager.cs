using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public UnityEvent OnRetry;
        public UnityEvent OnGameOver;
        public static UIManager Instance;
        public List<Screen> Screens;
        public Slider BossHpSlider;
        public HpBar BossHpBar;

        
        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
            BossHpBar = GameObject.FindWithTag("Boss")?.GetComponent<HpBar>();
            Debug.Log("UI LOADED");
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            BossHpBar = GameObject.FindWithTag("Boss")?.GetComponent<HpBar>();
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            MainManager.Instance.OnStartBossFight?.AddListener(UpdateSlider);
            BossHpBar?.onHit.AddListener(UpdateSlider);
            MainManager.Instance?.PlayerMachine?.HpComponent.onDeath?.AddListener(GameOver);
            OnRetry.AddListener(MainManager.Instance.ResetPlayerStats);
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            MainManager.Instance.OnStartBossFight?.RemoveListener(UpdateSlider);
            BossHpBar?.onHit.RemoveListener(UpdateSlider);
            MainManager.Instance?.PlayerMachine?.HpComponent.onDeath?.RemoveListener(GameOver);
            OnRetry?.RemoveListener(MainManager.Instance.ResetPlayerStats);

        }
        public void ToggleScreen(string ScreenName) => Screens.Find(_ => _.ScreenName == ScreenName)?.ScreenObject.SetActive(!Screens.Find(_ => _.ScreenName == ScreenName).ScreenObject.activeSelf);
        public void GameOver()
        {
            for (int i = 0; i < Screens.Count; i++)
            {
                if(Screens[i].ScreenObject.name == "Game Over Screen") Screens[i].ScreenObject.SetActive(true);
                else Screens[i].ScreenObject.SetActive(false);
            }
            MainManager.Instance.IsGameOver = true;
            OnGameOver?.Invoke();
        }

        public void Retry()
        {
            SceneLoader.Instance.LoadNextSceneAsync(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
            OnRetry?.Invoke();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void UpdateSlider()
        {
            BossHpSlider.maxValue = BossHpBar.MaxHealth;
            BossHpSlider.value = BossHpBar.CurrentHealth;
        }
    }

    [Serializable]
    public class Screen
    {
        public GameObject ScreenObject;
        public string ScreenName;
        
    }
}
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
        public static UIManager Instance;

        public UnityEvent OnRetry;
        public UnityEvent OnGameOver;
        public UnityEvent OnPause;
        public UnityEvent OnUnPause;
        public UnityEvent OnReturnToPlaying;
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
        public void ToggleScreen(string ScreenName)
        {
            Screens.Find(_ => _.ScreenName == ScreenName)?.ScreenObject.SetActive(!Screens.Find(_ => _.ScreenName == ScreenName).ScreenObject.activeSelf);
            //for (int i = 0; i < Screens.Count; i++)
            //{
            //    if (Screens[i].ScreenName == ScreenName) Screens[i].ScreenObject.SetActive(true);
            //    else Screens[i].ScreenObject.SetActive(false);
            //}
        }

        public void InvokeReturnToPlaying() => OnReturnToPlaying?.Invoke();

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
            SceneLoader.Instance.ReloadLevel();
            OnRetry?.Invoke();
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Pause() => OnPause?.Invoke();

        public void InvokeSetTimeScale(float time) => MainManager.Instance.SetTimeScale(time);

        public void UnPause() => OnUnPause?.Invoke();
        

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
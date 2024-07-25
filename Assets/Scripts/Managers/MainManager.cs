using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.Events;
using StateMachine.Player;
namespace Manager
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;
        public SettingsData Settings;
        public PlayerStats PlayerStatsSCO;
        public PlayerMonoStateMachine PlayerMachine => FindAnyObjectByType<PlayerMonoStateMachine>();

        public UnityEvent OnStartBossFight;



        public bool IsPaused()
        {
            if (Time.timeScale == 0) return true;
            else return false;
        }
        public bool IsGameOver = false;
        public bool BossFightStarted = false;
        private void Awake()
        {
            Debug.Log("MAIN SCENE LOADED");
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }
        private void Start()
        {
            Application.targetFrameRate = Settings.TargetFPS;
        }

        public void SetTimeScale(float time) => Time.timeScale = time;

        public void StartBossFight()
        {
            BossFightStarted = true;
            OnStartBossFight?.Invoke();
            UIManager.Instance.ToggleScreen("Boss Fight UI");
        }

        public void EndBossFight()
        {
            UIManager.Instance.ToggleScreen("Boss Fight UI");
        }


        public void ResetPlayerStats() => PlayerStatsSCO.Reset();

        private void OnApplicationQuit()
        {
            if(Application.isEditor)
            {
                PlayerStatsSCO.Reset();
            }
        }
    }
}


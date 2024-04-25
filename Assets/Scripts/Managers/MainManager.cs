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



        public bool IsPaused = false;
        public bool IsGameOver = false;
        public bool BossFightStarted = false;
        private void Awake()
        {
            //Debug.Log("MAIN SCENE LOADED");
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

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

        public void DebuggingTestFrom(GameObject obj)
        {
            Debug.Log("THIS DEBUG IS FROM " + obj.name);
        }


    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using StateMachine.Player;
namespace Manager
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;
        public PlayerStats PlayerStatsSCO;
        public PlayerMonoStateMachine PlayerMachine => FindAnyObjectByType<PlayerMonoStateMachine>();

        public bool IsPaused = false;
        public bool IsGameOver = false;
        private void Awake()
        {
            Debug.Log("MAIN SCENE LOADED");
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        public void ResetPlayerStats() => PlayerStatsSCO.Reset();

        private void OnApplicationQuit()
        {
            if(Application.isEditor)
            {
                PlayerStatsSCO.PlayerCurrentHealth = PlayerStatsSCO.PlayerMaxHealth;
            }
        }


    }
}


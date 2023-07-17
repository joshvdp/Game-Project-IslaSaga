using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
namespace Manager
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance;

        [SerializeField] PlayerStats PlayerStatsSCO;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }

        private void OnApplicationQuit()
        {
            if(Application.isEditor)
            {
                PlayerStatsSCO.PlayerCurrentHealth = PlayerStatsSCO.PlayerMaxHealth;
            }
        }


    }
}


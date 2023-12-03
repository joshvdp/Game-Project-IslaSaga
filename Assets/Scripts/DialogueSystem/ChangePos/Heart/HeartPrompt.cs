using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.Events;

namespace DialogueSystem
{
    public class HeartPrompt : MonoBehaviour
    {
        public PlayerStats playerStats;
        public float HealAmount;
        public UnityEvent onPicked;

        int index;

        
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
            if (collision.transform.tag != "Player") return;
            index = 16;
            DialogueHandler.Instance.EnableDialogue(index);
            playerStats.TakeHeal(HealAmount);
            Destroy(gameObject);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag != "Player") return;
            index = 16;
            DialogueHandler.Instance.EnableDialogue(index);
            playerStats.TakeHeal(HealAmount);
            Destroy(gameObject);
            onPicked.Invoke();

        }
    }
}


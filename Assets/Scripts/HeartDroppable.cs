using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.Events;

public class HeartDroppable : MonoBehaviour
{
    public PlayerStats playerStats;
    public float IncreaseMaxHpAmount;
    public UnityEvent onPicked;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.tag != "Player") return;
        playerStats.IncreaseMaxHP(IncreaseMaxHpAmount);
        Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player") return;
        playerStats.IncreaseMaxHP(IncreaseMaxHpAmount);
        Destroy(gameObject);
        onPicked.Invoke();
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.Events;

public class HeartDroppable : MonoBehaviour
{
    public PlayerStats playerStats;
    public float HealAmount;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Player") return;
        playerStats.TakeHeal(HealAmount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player") return;
        playerStats.TakeHeal(HealAmount);
        Destroy(gameObject);
    }
}

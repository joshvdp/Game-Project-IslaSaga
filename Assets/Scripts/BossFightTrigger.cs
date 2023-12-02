using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class BossFightTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        MainManager.Instance.StartBossFight();
        Destroy(gameObject);
    }
}

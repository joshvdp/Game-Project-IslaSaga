using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFX : MonoBehaviour
{
    public GameObject BloodParticle;
    
    public void Blood()
    {
        Instantiate(BloodParticle, transform.position, transform.rotation);
    }
}

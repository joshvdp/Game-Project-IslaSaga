using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpParticle : MonoBehaviour
{
    public GameObject ParticleJump;
    public void Jumped()
    {
        ParticleJump.SetActive(true);
    }

    public void DoneJump()
    {
        ParticleJump.SetActive(false);
    }
}

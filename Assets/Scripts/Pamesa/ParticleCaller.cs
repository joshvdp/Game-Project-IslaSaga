using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCaller : MonoBehaviour
{
    public GameObject LandParticle, SprintParticle, JumpParticle, Blood;

    //-----------Land------------//
    public void Landed()
    {
        LandParticle.SetActive(true);
    }

    //-----------Sprint------------//
    public void Sprinting()
    {
        SprintParticle.SetActive(true);
    }
    public void DoneSprinting()
    {
        SprintParticle.SetActive(false);
    }

    //-----------Jump------------//
    public void Jumped()
    {
        JumpParticle.SetActive(true);
    }

    //-----------Blood------------//
    public void HitEnemy()
    {
        Blood.SetActive(true);
    }
}

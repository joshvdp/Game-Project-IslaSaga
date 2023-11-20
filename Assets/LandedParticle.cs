using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandedParticle : MonoBehaviour
{
    public GameObject LandParticle;

    public void Landed()
    {
        LandParticle.SetActive(true);
    }
}

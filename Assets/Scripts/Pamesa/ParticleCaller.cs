using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCaller : MonoBehaviour
{
    public GameObject Particle;

    public void Active()
    {
        Particle.SetActive(true);
    }
    public void Unactive()
    {
        Particle.SetActive(false);
    }
}

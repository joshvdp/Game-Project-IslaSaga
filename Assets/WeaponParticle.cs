using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;

namespace VFX
{
    public class WeaponParticle : MonoBehaviour
    {
        public delegate void ParticleEvent();
        public static ParticleEvent Swing, SwingDone;
        public GameObject  SpawnPoint;
        public ParticleSystem Trails;
        private WeaponVFXCaller ParticleVFX;

        private void OnEnable()
        {
            Swing += SwingParticle;
            SwingDone += DoneSwing;
        }
        private void OnDisable()
        {
            Swing -= SwingParticle;
            SwingDone -= DoneSwing;
        }
        private void Start()
        {
            ParticleVFX = GetComponent<WeaponVFXCaller>();
            //Trails.SetActive(false);
        }
        private void SwingParticle()
        {
            Debug.Log("SwingParticle");
            //ParticleVFX1 = Instantiate(Trails, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            /*ParticleVFX1.SetActive(true);
            ParticleVFX1.transform.SetParent(transform);*/
            Trails.gameObject.SetActive(true);
        }
        private void DoneSwing()
        {
            Debug.Log("PASSED");
            Trails.gameObject.SetActive(false);
            //Trails.Clear();
        }


    }
}
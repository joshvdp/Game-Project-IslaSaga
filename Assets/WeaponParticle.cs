using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;

namespace VFX
{
    public class WeaponParticle : MonoBehaviour
    {
        public delegate void ParticleEvent();
        public static ParticleEvent Swing;
        public GameObject Trails, SpawnPoint;

        private WeaponVFXCaller ParticleVFX;

        private void OnEnable()
        {
            Swing += SwingParticle;
        }
        private void OnDisable()
        {
            Swing -= SwingParticle;
        }
        private void Start()
        {
            ParticleVFX = GetComponent<WeaponVFXCaller>();
            //Trails.SetActive(false);
        }
        private void SwingParticle()
        {
            Debug.Log("SwingParticle");
            GameObject ParticleVFX = Instantiate(Trails, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            //Trails.SetActive(true);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class PlayerVFXHandler : MonoBehaviour
    {
        public delegate void PlayerVisualEffects();
        public static PlayerVisualEffects Regen, PowerUp, Special;
        public GameObject ParticleHeal, ParticleLevelUp, ParticleAbility;

        private PlayerVFXCaller Particle;

        private void OnEnable()
        {
            Regen += Heal;
            PowerUp += LevelUp;
            Special += Ability;
            
        }
        private void OnDisable()
        {
            Regen -= Heal;
            PowerUp -= LevelUp;
            Special -= Ability;

        }
        private void Start()
        {
            Particle = GetComponent<PlayerVFXCaller>();
        }
        private void Heal()
        {
            Debug.Log("Part");
            GameObject Particle = Instantiate(ParticleHeal, transform.position, transform.rotation);
        }
        private void LevelUp()
        {
            GameObject Particle = Instantiate(ParticleLevelUp, transform.position, transform.rotation);
        }
        private void Ability()
        {
            GameObject Particle = Instantiate(ParticleAbility, transform.position, transform.rotation);
        }
    }
}


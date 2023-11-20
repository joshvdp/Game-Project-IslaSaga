using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX
{
    public class BloodParticle : MonoBehaviour
    {
        public delegate void OnHitEvent();
        public static OnHitEvent Hit, HitDone;
        public GameObject SpawnPoint,Blood;
        //public ParticleSystem Blood;
        private BloodVFXCaller BloodVFX;

        private void OnEnable()
        {
            Hit += EnemyHit;
            HitDone += DoneHit;
        }
        private void OnDisable()
        {
            Hit -= EnemyHit;
            HitDone -= DoneHit;
        }
        private void Start()
        {
            
            BloodVFX = GetComponent<BloodVFXCaller>();
        }
        private void EnemyHit()
        {
            Debug.Log("ENEMYHIT");
            Blood.gameObject.SetActive(true);
            //Instantiate(Blood, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            //BloodVFX = Instantiate(Blood, transform.position, transform.rotation);
        }
        private void DoneHit()
        {
            Blood.gameObject.SetActive(false);
        }
    }
}

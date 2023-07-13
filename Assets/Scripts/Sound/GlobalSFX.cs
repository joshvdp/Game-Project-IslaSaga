using System;
using Unity.Mathematics;
using UnityEngine;

namespace Audio_Scripts
{
    public class GlobalSFX : MonoBehaviour
    {
        public static Action<Vector3> hitEvent;
        public static Action<Vector3, GameObject> grunt, death, bossScream, bossGrunt, bossDied, smashRocks, enraged;

        public GameObject wallShot, enemyHit;

        private AudioSource audioSource;

        private void OnEnable()
        {
            //wallEvent += Wall;
            hitEvent += Flesh;
            
        }

        private void OnDisable()
        {
            //wallEvent -= Wall;
            hitEvent -= Flesh;
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }


        private void Flesh(Vector3 position) => Instantiate(enemyHit, position, quaternion.identity);

        //private void Wall(Vector3 position) => Instantiate(wallShot, position, quaternion.identity);
    }


}

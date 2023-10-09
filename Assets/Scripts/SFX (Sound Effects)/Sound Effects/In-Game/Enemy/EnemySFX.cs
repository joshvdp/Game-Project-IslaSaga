using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace SoundFX
{
    public class EnemySFX: MonoBehaviour
    {
        public delegate void statusEvent();
        public static statusEvent onGrunt, onDeath, onGrowl, onHit;

        public GameObject Grunt, Died, Growl, Hit;

        private EnemyHpHandler sound;

        private void OnEnable()
        {
            onGrunt += Grunting;
            onDeath += Death;
            onGrowl += Growling;
            onHit += TakeHit;
        }

        private void OnDisable()
        {
            onGrunt -= Grunting;
            onDeath -= Death;
            onGrowl -= Growling;
            onHit -= TakeHit;
        }
        private void Start()
        {
            sound = GetComponent<EnemyHpHandler>();
        }

        public void Grunting()
        {
            Debug.Log("Grunt");
            //GameObject sound = Instantiate(Grunt, transform.position, transform.rotation);
        }
        public void Death()
        {
            GameObject sound = Instantiate(Died, transform.position, transform.rotation);
        }
        public void Growling()
        {
            Debug.Log("Growl");
            //GameObject sound = Instantiate(Growl, transform.position, transform.rotation);
        }
        public void TakeHit()
        {
            GameObject sound = Instantiate(Hit, transform.position, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

namespace SoundFX
{
    public class EnemySFX: MonoBehaviour
    {
        public delegate void statusEvent();
        public static statusEvent onGrunt, onDied, onGrowl;

        public GameObject Grunt, Died, Growl;

        private EnemyHpHandler sound;

        private void OnEnable()
        {
            onGrunt += Grunting;
            onDied += Death;
            onGrowl += Growling;
            
        }

        private void OnDisable()
        {
            onGrunt -= Grunting;
            onDied -= Death;
            onGrowl -= Growling;
        }
        private void Start()
        {
            sound = GetComponent<EnemyHpHandler>();
        }

        public void Grunting()
        {
            Debug.Log("I am Grunting");
            //GameObject hit = Instantiate(Grunt, transform.position, transform.rotation);
        }
        public void Death()
        {
            Debug.Log("I Died");
            //GameObject hit = Instantiate(Died, transform.position, transform.rotation);
        }
        public void Growling()
        {
            Debug.Log("Grrrr");
            //GameObject hit = Instantiate(Growl, transform.position, transform.rotation);
        }
    }


}

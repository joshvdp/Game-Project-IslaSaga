using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Combat
{
    public class WeaponSound : MonoBehaviour
    {
        public delegate void attackEvent();
        public static attackEvent attackEvent1, attackEvent2, attackEvent3, attackEvent4;

        //public AudioClip reload;
        //public AudioSource audiosource;

        public GameObject sound1, sound2, sound3, sound4;

        private PlayerCombat hit;

        private void OnEnable()
        {
            //attack1 += attack;
            attackEvent1 += Attack1;
            attackEvent2 += Attack2;
            attackEvent3 += Attack3;
            attackEvent4 += Attack4;
        }
        private void OnDisable()
        {
            //attack1 -= attack;
            attackEvent1 -= Attack1;
            attackEvent2 -= Attack2;
            attackEvent3 -= Attack3;
            attackEvent4 -= Attack4;
        }
        private void Start()
        {
            hit = GetComponent<PlayerCombat>();
        }
        private void Attack1()
        {
            Debug.Log("Sound 1 Plays");
            GameObject hit = Instantiate(sound1, transform.position, transform.rotation);
        }
        private void Attack2()
        {
            Debug.Log("Sound 2 Plays");
            GameObject hit = Instantiate(sound2, transform.position, transform.rotation);
        }
        private void Attack3()
        {
            Debug.Log("Sound 3 Plays");
            GameObject hit = Instantiate(sound3, transform.position, transform.rotation);
        }
        private void Attack4()
        {
            Debug.Log("Sound 4 Plays");
            GameObject hit = Instantiate(sound4, transform.position, transform.rotation);
        }

        /*private void attack()
        {
            audiosource.PlayOneShot(reload);
        }*/
    }
}


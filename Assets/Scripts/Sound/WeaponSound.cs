using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Combat
{
    public class WeaponSound : MonoBehaviour
    {
        public delegate void attackEvent();
        public static attackEvent attackEvent1, attackEvent2, attackEvent3, attackEvent4;


        public GameObject sound1, sound2, sound3, sound4;

        private PlayerCombat hit;

        private void OnEnable()
        {
            attackEvent1 += Attack1;
            attackEvent2 += Attack2;
            attackEvent3 += Attack3;
            attackEvent4 += Attack4;
        }
        private void OnDisable()
        {
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
            Debug.Log("Type 1");
            GameObject hit = Instantiate(sound1, transform.position, transform.rotation);
        }
        private void Attack2()
        {
            Debug.Log("Type 1");
            GameObject hit = Instantiate(sound2, transform.position, transform.rotation);
        }
        private void Attack3()
        {
            Debug.Log("Type 3");
            GameObject hit = Instantiate(sound3, transform.position, transform.rotation);
        }
        private void Attack4()
        {
            Debug.Log("Type 4");
            GameObject hit = Instantiate(sound4, transform.position, transform.rotation);
        }

    }
}


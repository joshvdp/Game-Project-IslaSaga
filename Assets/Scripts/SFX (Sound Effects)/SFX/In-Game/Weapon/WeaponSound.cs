using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;

namespace AudioSoundEvents
{
    public class WeaponSound : MonoBehaviour
    {
        public delegate void attackEvent();
        public static attackEvent attackEvent1, attackEvent2, attackEvent3, attackEvent4;
        public GameObject AttackSwing, SPIN, Ability, Hit;

        private PlayerSound Sound;

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
            Sound = GetComponent<PlayerSound>();
        }
        private void Attack1()
        {
            //Debug.Log("Attack 1");
            GameObject Sound = Instantiate(AttackSwing, transform.position, transform.rotation);
        }
        private void Attack2()
        {
            //Debug.Log("Attack 2");
            GameObject Sound = Instantiate(SPIN, transform.position, transform.rotation);
        }
        private void Attack3()
        {
            //Debug.Log("Attack 3");
            GameObject Sound = Instantiate(Ability, transform.position, transform.rotation);
        }
        private void Attack4()
        {
            //Debug.Log("SPIN");
            GameObject Sound = Instantiate(Hit, transform.position, transform.rotation);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;

namespace AudioSoundEvents
{
    public class WeaponEnemySound : MonoBehaviour
    {
        public delegate void attackEvent();
        public static attackEvent attackEvent1, attackEvent2, attackEvent3, attackEvent4;
        public GameObject AttackSwing1, AttackSwing2, AttackSwing3, AttackSwing4;
        private BossAudioSound Sound;

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
            Sound = GetComponent<BossAudioSound>();
        }
        private void Attack1()
        {
            GameObject Sound = Instantiate(AttackSwing1, transform.position, transform.rotation);
        }
        private void Attack2()
        {
            GameObject Sound = Instantiate(AttackSwing2, transform.position, transform.rotation);
        }
        private void Attack3()
        {
            GameObject Sound = Instantiate(AttackSwing3, transform.position, transform.rotation);
        }
        private void Attack4()
        {
            GameObject Sound = Instantiate(AttackSwing4, transform.position, transform.rotation);
        }
    }
}
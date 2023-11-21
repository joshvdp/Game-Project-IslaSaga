using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using StateMachine.Player;
//using Unity.PlasticSCM.Editor.WebApi;

namespace AudioSoundEvents
{
    public class BossAudioSound : MonoBehaviour
    {
        [Header("MIXER CATEGORY: ")]
        [SerializeField] public AudioSource SFX;
        //public UnityEvent PlayerSoundEvent;

        #region ENEMY BOSS

        [Header("BOSS ENEMY")] 
        public GameObject FootStepRight;
        public GameObject FootStepLeft;
        public GameObject Death;
        public GameObject Jump;
        public GameObject SmokeBomb;
        
        public void MoveRightFoot()
        {
            Instantiate(FootStepRight, transform.position, transform.rotation);
        }
        public void MoveLeftFoot()
        {
            Instantiate(FootStepLeft, transform.position, transform.rotation);
        }
        public void Jumps ()
        {
            Instantiate(Jump, transform.position, transform.rotation);
            Jumping();
        }
        public void Dies ()
        {
            Instantiate(Death, transform.position, transform.rotation);
        }
        public void Dissappear()
        {
            Instantiate(SmokeBomb, transform.position, transform.rotation);
            Debug.Log("BOMB");
        }
        #endregion

        #region ENEMY COMBAT
        
        public void Attack1()
        {
            WeaponEnemySound.attackEvent1?.Invoke();
        }
        public void Attack2()
        {
            WeaponEnemySound.attackEvent2?.Invoke();
        }
        public void Attack3()
        {
            WeaponEnemySound.attackEvent3?.Invoke();
        }
        public void Attack4()
        {
            WeaponEnemySound.attackEvent4?.Invoke();
        }

        #endregion

        #region ENEMY SOUND BITES
        
        [Header("ENEMY SOUND BITES:")]
        public AudioClip Hit;
        public AudioClip Attacking;
        public AudioClip LandOnGround;
        public AudioClip Interacts;
        public AudioClip JumpJump;
        public AudioClip Dead;

        public void Grunting ()
        {
            SFX.PlayOneShot(Attacking);
        }

        public void Hurt()
        {
            SFX.PlayOneShot(Hit);
        }

        public void Jumping ()
        {
            SFX.PlayOneShot(JumpJump);
        }

        public void Landing()
        {
            SFX.PlayOneShot(LandOnGround);
        }

        public void Dying()
        {
            Debug.Log("Death Voice");
        }

        public void Interact()
        {
            Debug.Log("Interact");
        }

        #endregion
    }
}
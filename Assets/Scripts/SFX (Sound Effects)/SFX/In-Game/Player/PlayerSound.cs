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
    public class PlayerSound : MonoBehaviour
    {
        [Header("MIXER CATEGORY: ")]
        [SerializeField] public AudioSource SFX;
        //public UnityEvent PlayerSoundEvent;

        [SerializeField] PlayerMonoStateMachine PlayerMachine;
        
        #region PLAYER

        public void Start()
        {
            if (PlayerMachine.CurrentState.Data.name == "Player Landed")
            {
                Debug.Log("Machine Landed");
            }
        }

        [Header("PLAYER:")] 
        public GameObject FootStep;
        public GameObject Death;
        public GameObject Jump;
        public GameObject Landed;
        
        public void Moving()
        {
            Instantiate(FootStep, transform.position, transform.rotation);
        }

        public void Jumps ()
        {
            Instantiate(Jump, transform.position, transform.rotation);
            //Debug.Log("JUMP");
            Jumping();
        }

        public void Dies ()
        {
            //Instantiate(Death, transform.position, transform.rotation);
            Debug.Log("DEAD BODY THUD");
        }

        public void Land()
        {
            Instantiate(Landed, transform.position, transform.rotation);
            
            Landing();
        }
        
        

        #endregion

        #region PLAYER COMBAT
        
        public void Attack1()
        {
            WeaponSound.attackEvent1?.Invoke();
        }
        public void Attack2()
        {
            WeaponSound.attackEvent2?.Invoke();
        }
        public void Attack3()
        {
            WeaponSound.attackEvent3?.Invoke();
        }
        public void Attack4()
        {
            WeaponSound.attackEvent4?.Invoke();
        }

        #endregion

        #region PLAYER SOUND BITES
        
        [Header("PLAYER SOUND BITES:")]
        public AudioClip Hit;
        public GameObject Attacking;
        public AudioClip LandOnGround;
        public AudioClip Interacts;
        public AudioClip JumpJump;
        public AudioClip Dead;

        public void Grunting ()
        {
            
            Instantiate(Attacking, transform.position, transform.rotation);
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
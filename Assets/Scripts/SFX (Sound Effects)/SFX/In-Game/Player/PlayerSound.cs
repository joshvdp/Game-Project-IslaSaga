using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

namespace AudioSoundEvents
{
    public class PlayerSound : MonoBehaviour
    {
        [Header("MIXER CATEGORY: ")]
        [SerializeField] public AudioSource SFX;
        //public UnityEvent SoundBite;
        
        #region PLAYER

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
            Dying();
        }

        public void Land()
        {
            Instantiate(Landed, transform.position, transform.rotation);
            //Debug.Log("LAND THUD");
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
        public AudioClip TakeHit;
        public GameObject Attacking;
        public AudioClip LandOnGround;
        public AudioClip Interacts;
        public AudioClip JumpJump;
        public AudioClip Dead;

        public void Grunting ()
        {
            //SoundBite.Invoke();
            Instantiate(Attacking, transform.position, transform.rotation);
        }
        public void Jumping ()
        {
            SFX.PlayOneShot(JumpJump);
            Debug.Log("Jump Voice");
        }

        public void Landing()
        {
            Debug.Log("Land Voice");
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
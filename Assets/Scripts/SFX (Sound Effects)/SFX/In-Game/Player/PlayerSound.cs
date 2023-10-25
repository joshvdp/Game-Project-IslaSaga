using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace AudioSoundEvents
{
    public class PlayerSound : MonoBehaviour
    {
        /*[Header("MIXER CATEGORY: ")]
        [SerializeField] public AudioSource SFX;*/

        #region PLAYER

        [Header("PLAYER: ")] 
        public GameObject FootStep;
        public GameObject Grunt;
        public GameObject Death;
        public GameObject Jump;
        public GameObject Landed;

        public void Moving()
        {
            Instantiate(FootStep, transform.position, transform.rotation);
        }

        public void Grunting ()
        {
            //Instantiate(Grunt, transform.position, transform.rotation);
            Debug.Log("ANGRY AS FCK");
        }
        
        public void Jumps ()
        {
            //Instantiate(Jump, transform.position, transform.rotation);
            Debug.Log("JUMP");
        }

        public void Dies ()
        {
            //Instantiate(Death, transform.position, transform.rotation);
            Debug.Log("NIGGA DIED");
        }

        public void Land()
        {
            //Instantiate(Landed, transform.position, transform.rotation);
            Debug.Log("NIGGA LANDED");
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
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;

namespace AudioSoundEvents
{
    public class AttackingSFX : MonoBehaviour
    {
        public WeaponSound weaponSoundCS => GetComponent<WeaponSound>();
        public void Attack1()
        {
            weaponSoundCS.attackEvent1?.Invoke();
        }
        public void Attack2()
        {
            weaponSoundCS.attackEvent2?.Invoke();
        }
        public void Attack3()
        {
            weaponSoundCS.attackEvent3?.Invoke();
        }
        public void Attack4()
        {
            weaponSoundCS.attackEvent4?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;

namespace AudioSoundEvents
{
    public class AttackingSFX : MonoBehaviour
    {
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
    }
}

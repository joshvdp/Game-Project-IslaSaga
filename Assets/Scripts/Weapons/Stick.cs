using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using UnityEngine.Events;
using StateMachine.Player;
namespace Items.Weapon
{
    public class Stick : MonoBehaviour, IWeapon
    {
        public UnityEvent OnTargetHit;
        public UnityEvent OnNoTargetHit;
        [SerializeField] float WeaponDamage;
        [SerializeField] float WeaponSequenceResetTime;
        [SerializeField] Quaternion WeaponRotationVar;
        [SerializeField] DamageType WeaponDmgType;
        public DamageType WeaponDamageType { get { return WeaponDmgType; } set { WeaponDmgType = value; } }
        public float Damage { get { return WeaponDamage; } set { WeaponDamage = value; } }

        public float SequenceResetTime { get => WeaponSequenceResetTime; set { WeaponSequenceResetTime = value; } }

        public Quaternion WeaponRotation { get => WeaponRotationVar; set { WeaponRotationVar = value; } }

        [HideInInspector] public PlayerMonoStateMachine Holder;
        public PlayerMonoStateMachine holder { get => Holder; set => Holder = value; }
        public UnityEvent OnTargetIsHit { get => OnTargetHit; set => OnTargetHit = value; }
        public UnityEvent OnNoTargetIsHit { get => OnNoTargetHit; set => OnNoTargetHit = value; }

        private void OnDisable()
        {
            OnTargetIsHit?.RemoveListener(Attack);
            for (int i = 0; i < holder.AttackCollidersHandler.Colliders.Length; i++)
            {
                holder.AttackCollidersHandler.Colliders[i].OnTargetHit?.RemoveListener(InvokeOnTargetHit);
                holder.AttackCollidersHandler.Colliders[i].OnNoTargetHit?.RemoveListener(InvokeOnNoTargetHit);
            }
        }


        public void Attack()
        {
            Debug.Log("Stick attack");
        }
        public GameObject GetGameobject() => gameObject;
        public void SubscribeEvents()
        {
            OnTargetIsHit.AddListener(Attack);
            for (int i = 0; i < holder.AttackCollidersHandler.Colliders.Length; i++)
            {
                holder.AttackCollidersHandler.Colliders[i].OnTargetHit.AddListener(InvokeOnTargetHit);
                holder.AttackCollidersHandler.Colliders[i].OnNoTargetHit.AddListener(InvokeOnNoTargetHit);
            }
        }
        public void InvokeOnTargetHit() => OnTargetIsHit?.Invoke();
        public void InvokeOnNoTargetHit() => OnNoTargetIsHit?.Invoke();
    }

}

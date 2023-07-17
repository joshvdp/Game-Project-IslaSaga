using System.Collections.Generic;
using UnityEngine;
using Interface;

namespace Items.Weapon
{
    public class AmethystSwordCS : MonoBehaviour, IWeapon
    {
        [SerializeField] float WeaponDamage;
        public float Damage { get {return WeaponDamage;} set {WeaponDamage = value; } }

        private void OnEnable()
        {
            PlayerAnimEvents.OnWeaponAttack += Attack;
        }
        private void OnDisable()
        {
            PlayerAnimEvents.OnWeaponAttack -= Attack;
        }
        public void Attack(List<GameObject> Targets, int Sequence)
        {

            for (int i = 0; i < Targets.Count; i++)
            {
                 if (Targets[i].gameObject != null)
                {
                    IDamageable EnemyHp = Targets[i].GetComponent<IDamageable>();
                    if (EnemyHp != null) EnemyHp.Hit(Damage);
                } 
            }
            
           
        }
    }
}


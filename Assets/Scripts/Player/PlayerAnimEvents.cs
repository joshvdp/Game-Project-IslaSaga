using System.Collections.Generic;
using UnityEngine;
using Player.Combat;
using System;
public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField] PlayerCombat PlayerCombatCS;
    [SerializeField] PlayerAttackRangeDetect PlayerAttackRangeCS;
    public static event Action<List<GameObject>, int> OnWeaponAttack;
    void CheckAttackSequence()
    {
        PlayerCombatCS.CheckAttackSequence();
    }

    void WeaponAttack()
    {
        OnWeaponAttack?.Invoke(PlayerAttackRangeCS.EnemiesInRange, PlayerCombatCS.AttackSequence);
    }
}

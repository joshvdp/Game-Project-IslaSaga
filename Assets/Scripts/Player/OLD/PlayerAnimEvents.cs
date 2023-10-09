using System.Collections.Generic;
using UnityEngine;
using Player.Combat;
using System;
public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField] PlayerReferences References;
    public static event Action<List<GameObject>, int> OnWeaponAttack;
    void CheckAttackSequence()
    {
        References.PlayerCombatCS.CheckAttackSequence();
    }

    void WeaponAttack()
    {
        OnWeaponAttack?.Invoke(References.PlayerAttackRangeCS.EnemiesInRange, References.PlayerCombatCS.AttackSequence);
    }
}

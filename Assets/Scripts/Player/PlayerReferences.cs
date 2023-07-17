using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Movement;
using Player.Combat;
using Player.Animation;
using Player;
public class PlayerReferences : MonoBehaviour
{
    public ControlBindings Controls;
    public PlayerStats PlayerStatsCS;
    public PlayerAnimationHandler PlayerAnimCS;
    public PlayerMovement PlayerMoveCS;
    public PlayerCombat PlayerCombatCS;
    public PlayerHpHandler PlayerHpCS;
    public PlayerAttackRangeDetect PlayerAttackRangeCS;
}

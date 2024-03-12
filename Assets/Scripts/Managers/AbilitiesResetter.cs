using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Player;
using StateMachine.Enemy;
using StateMachine.Enemy.State;

public class AbilitiesResetter : MonoBehaviour
{
    public PlayerMachineData PlayerJump;

    public MarquisFollow MarquisRun;
    public MarquisAttack MarquisAttackLight;
    public MarquisAttack MarquisAttackHeavy;
    public MarquisChooseAttack MarquisChooseAttack;
    [SerializeField] bool CanDoubleJump;
    [SerializeField] float MarquisAttackAnimSpeed;
    [SerializeField] float MarquisMoveSpeed;
    [SerializeField] float MarquisChooseSpeed;
    [SerializeField] float MarquisAttackLightDamage;
    [SerializeField] float MarquisAttackHeavyDamage;

    private void Awake()
    {
        PlayerJump.isUnlocked = CanDoubleJump;

        MarquisRun.speed = MarquisMoveSpeed;
        MarquisAttackLight.animationSpeed = MarquisAttackAnimSpeed;
        MarquisAttackHeavy.animationSpeed = MarquisAttackAnimSpeed;
        MarquisChooseAttack.chooseTime = MarquisChooseSpeed;

        MarquisAttackLight.damage = MarquisAttackLightDamage;
        MarquisAttackHeavy.damage = MarquisAttackHeavyDamage;

    }
}

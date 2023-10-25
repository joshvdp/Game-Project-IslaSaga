using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Player.Controls;
using UnityEngine;
namespace StateMachine.Player.State
{
    [CreateAssetMenu(fileName = "Player Attack", menuName = "State Machine/Player/State/Attack")]
    public class PlayerAttack : PlayerMachineData
    {
        [SerializeField, Foldout("Attack")] string colliderName;
        [SerializeField, Foldout("Attack")] TargetingType targetingType;

        public TargetingType AttackTargetingType => targetingType;
        public string ColliderName => colliderName;
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerAttackFunctions(machine, this);
        }
    }
    public class PlayerAttackFunctions : PlayerMachineFunctions
    {
        string ColliderName;
        
        float Damage;
        float SequenceResetTime;
        TargetingType AttackTargetingType;
        public PlayerAttackFunctions(PlayerMonoStateMachine machine, PlayerAttack data) : base(machine, data)
        {
            if (machine.PlayerInputs.PlatformType == PlatformType.PC) machine.FaceDirectionOfMousePos();
            else if (machine.PlayerInputs.PlatformType == PlatformType.Mobile) machine.FaceToNearestEnemy();

            machine.PlayerRb.velocity = Vector3.zero;
            ColliderName = data.ColliderName;

            AttackTargetingType = data.AttackTargetingType;
            switch (AttackTargetingType)
            {
                case TargetingType.ALL_TARGETS:
                    machine.AnimationEvents.FindEvent("On Attack Frame").AddListener(AttackAllTargets);
                    break;
                case TargetingType.NEAREST_TARGET:
                    machine.AnimationEvents.FindEvent("On Attack Frame").AddListener(AttackNearestTarget);
                    break;
            }
            Damage = machine.CurrentWeaponDamage;
            switch (machine.AttackSequence)
            {
                case 0:
                    Damage *= 1.2f;
                    break;
                case 1:
                    Damage *= 1.4f;
                    break;
                case 2:
                    Damage *= 1.5f;
                    break;
                case 3:
                    Damage *= 1.5f;
                    break;

            }
            
        }
        public void AttackAllTargets() => machine.AttackCollidersHandler.FindCollider(ColliderName).AttackAllTargets(Damage, machine.CurrentWeaponDamageType);
        public void AttackNearestTarget() => machine.AttackCollidersHandler.FindCollider(ColliderName).AttackNearestTarget(Damage, machine.CurrentWeaponDamageType);
        public override void Discard()
        {
            base.Discard();
            machine.AttackSequence++;

            machine.AttackSequenceReset(machine.CurrentWeaponSequenceResetTimer);
            switch (AttackTargetingType)
            {
                case TargetingType.ALL_TARGETS:
                    machine.AnimationEvents.FindEvent("On Attack Frame").RemoveListener(AttackAllTargets);
                    break;
                case TargetingType.NEAREST_TARGET:
                    machine.AnimationEvents.FindEvent("On Attack Frame").RemoveListener(AttackNearestTarget);
                    break;
            }
        }
    }

    public enum TargetingType
    {
        ALL_TARGETS,
        NEAREST_TARGET
    }
}
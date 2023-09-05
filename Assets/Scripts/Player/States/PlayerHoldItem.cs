using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Player.State
{
    [CreateAssetMenu(fileName = "Player Item Hold", menuName = "State Machine/Player/State/Item Hold")]
    public class PlayerHoldItem : PlayerMachineData
    {
        [SerializeField, Foldout("Hold Item")] private float moveSpeed;
        [SerializeField, Foldout("Hold Item")] private float itemFollowSpeed;

        public float MoveSpeed => moveSpeed;
        public float ItemFollowSpeed => itemFollowSpeed;
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerHoldItemFunctions(machine, this);
        }
    }
    public class PlayerHoldItemFunctions : PlayerMachineFunctions
    {
        float MoveSpeed;
        float ItemFollowSpeed;

        public GameObject ItemBeingHeld;
        public PlayerHoldItemFunctions(PlayerMonoStateMachine machine, PlayerHoldItem data) : base(machine, data)
        {
            MoveSpeed = data.MoveSpeed;
            ItemBeingHeld = machine.PickUpRange.NearestGameobject();
            ItemFollowSpeed = data.ItemFollowSpeed;
        }
        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();
            machine.PlayerRb.velocity = machine.MoveVelocityInputs * MoveSpeed;
            machine.RotateTowardsMovement(300f, false);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            HoldItem();
        }

        public override void Discard()
        {
            base.Discard();
            DropItem();
        }

        void HoldItem() => ItemBeingHeld.transform.position = Vector3.Lerp(ItemBeingHeld.transform.position, machine.ItemHoldPosition.position, ItemFollowSpeed * Time.deltaTime);

        void DropItem() => ItemBeingHeld = null;

    }
}
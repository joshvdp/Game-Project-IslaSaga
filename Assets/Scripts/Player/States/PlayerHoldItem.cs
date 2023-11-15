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
        [SerializeField, Foldout("Hold Item")] private float maxDistance;

        public float MoveSpeed => moveSpeed;
        public float ItemFollowSpeed => itemFollowSpeed;
        public float MaxDistance => maxDistance;
        public override PlayerMachineFunctions Initialize(PlayerMonoStateMachine machine)
        {
            return new PlayerHoldItemFunctions(machine, this);
        }
    }
    public class PlayerHoldItemFunctions : PlayerMachineFunctions
    {
        float MoveSpeed;
        float ItemFollowSpeed;
        float MaxDistance;
        public PlayerHoldItemFunctions(PlayerMonoStateMachine machine, PlayerHoldItem data) : base(machine, data)
        {
            MoveSpeed = data.MoveSpeed;
            if(CanPickup()) machine.ItemPickedUpRb = machine.PickUpRange.NearestGameobject().GetComponent<Rigidbody>();
            Checker();
            ItemFollowSpeed = data.ItemFollowSpeed;
            machine.ItemPickedUpRb.GetComponent<Collider>().excludeLayers |= 1 << machine.gameObject.layer;
            MaxDistance = data.MaxDistance;
            machine.MaxHoldDistance = MaxDistance;
        }
        public override void StateFixedUpdate()
        {
            base.StateFixedUpdate();
            machine.MoveHorizontal(MoveSpeed);
            machine.RotateTowardsMovement(300f, false);
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            Checker();
            HoldItem();
        }

        void HoldItem()
        {
            //ItemBeingHeld.transform.position = Vector3.Lerp(ItemBeingHeld.transform.position, machine.ItemHoldPosition.position, ItemFollowSpeed * Time.deltaTime);
            Vector3 Direction = machine.ItemHoldPosition.position - machine.ItemPickedUpRb.transform.position;
            float Distance = Direction.magnitude;
            machine.ItemPickedUpRb.rotation = machine.transform.rotation;
            machine.ItemPickedUpRb.velocity = Direction * ItemFollowSpeed * 2;
        }

        void DropItem()
        {
            if (machine.ItemPickedUpRb == null) return;
            machine.ItemPickedUpRb.velocity = Vector3.zero;
            machine.ItemPickedUpRb.useGravity = true;
        }

        void Checker()
        {
            Debug.Log((Vector3.Distance(machine.ItemPickedUpRb.position, machine.transform.position) > machine.MaxHoldDistance) + " BECAUSE " + Vector3.Distance(machine.ItemPickedUpRb.position, machine.transform.position) + " AND " + MaxDistance);
            if (machine.ItemPickedUpRb != null) machine.ItemPickedUpRb.useGravity = false;

            //if (Vector3.Distance(machine.ItemPickedUpRb.position, machine.transform.position) > machine.MaxHoldDistance)
            //{
            //   machine.OnEndstate?.Invoke();
            //}
        }

        bool CanPickup()
        {
            if (machine.ItemPickedUpRb == null) return true;
            else if (!GameObject.ReferenceEquals(machine.ItemPickedUpRb.gameObject, machine.PickUpRange.NearestGameobject()) && !machine.CurrentState.Data.name.Contains("Item Hold")) return true;
            else return false;

        }

        public override void Discard()
        {
            base.Discard();
            DropItem();
            machine.ItemPickedUpRb.GetComponent<Collider>().excludeLayers &= ~(1 << machine.gameObject.layer);
        }
    }
}
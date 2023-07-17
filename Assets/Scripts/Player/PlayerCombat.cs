using System.Collections;
using UnityEngine;
using Player.Movement;

namespace Player.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] PlayerReferences References;
        [SerializeField] ControlBindings Controls;
        [SerializeField] Transform WeaponHolder;
        [SerializeField] PlayerMovement PlayerMovementCS;
        [SerializeField] PlayerPickUp PlayerPickUpCS;
        [SerializeField] GameObject AttackRange;
        [SerializeField] Camera PlayerCam;
        [SerializeField] LayerMask RayHittableLayers;

        [Header("Attack Variables")]
        public bool IsAttacking;
        public int AttackSequence = 1;
        [SerializeField] float SequenceResetTime;
        private void Update()
        {
            GetInput();
        }

        void GetInput()
        {
            if (Input.GetKeyDown(Controls.Attack1Key) && !IsAttacking)
            {
                FaceDirectionOfMousePos();
                Attack();
                soundUpdate();
            }
        }
        void Attack()
        {
            StopCoroutine("AttackSequenceReset");
            References.PlayerAttackRangeCS.UpdateList();
            ChangePlayerMovement(0f);
            PlayerPickUpCS.DropItem();
            PlayerMovementCS.IsRunning = false;
            PlayerMovementCS.IsWalking = false;
            PlayerMovementCS.IsIdle = false;
            IsAttacking = true;
            AttackSequence++;
            StartCoroutine("AttackSequenceReset");
        }

        void FaceDirectionOfMousePos()
        {
            Ray MouseRay = PlayerCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(MouseRay, out RaycastHit rayCastHit, RayHittableLayers))
            {
                Vector3 DirectionToLookAt = new Vector3(rayCastHit.point.x, transform.position.y, rayCastHit.point.z);
                transform.rotation = Quaternion.LookRotation(DirectionToLookAt - transform.position, Vector3.up);
            }

        }
        public void CheckAttackSequence()
        {
            
            if (AttackSequence >= 4)
            {
                AttackSequence = 0;
                IsAttacking = false;
            }
            PlayerMovementCS.IsIdle = true;
            IsAttacking = false;
            ChangePlayerMovement(1f);
        }

        void ChangePlayerMovement(float value)
        {
            PlayerMovementCS.PlayerRb.velocity *= value;
            PlayerMovementCS.MoveValueHandler = value;
        }

        IEnumerator AttackSequenceReset()
        {
            yield return new WaitForSeconds(SequenceResetTime);
            IsAttacking = false;
            AttackSequence = 0;
        }
        public void soundUpdate ()
        {
            if (AttackSequence == 1)
            {
                WeaponSound.attackEvent1?.Invoke();
            }
            else if (AttackSequence == 2)
            {
                WeaponSound.attackEvent2?.Invoke();
            }
            else if (AttackSequence == 3)
            {
                WeaponSound.attackEvent3?.Invoke();
            }
            else if (AttackSequence == 4)
            {
                WeaponSound.attackEvent4?.Invoke();
            }
        }
           
    }
}

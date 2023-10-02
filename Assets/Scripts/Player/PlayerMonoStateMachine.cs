using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
using System;
using NaughtyAttributes;
using Player.Controls;
using Core;
using Interface;
using Player;
using AudioSoundEvents;
namespace StateMachine.Player
{
    [RequireComponent(typeof(PlayerInputs))]
    public class PlayerMonoStateMachine : StateMachineHandler<PlayerMachineData, PlayerMachineFunctions>
    {
        [SerializeField, Foldout("Transform")] private Transform FeetRayStart;
        [SerializeField, Foldout("Transform")] public Transform ItemHoldPosition;
        [SerializeField] LayerMask NavigatableAreas;
        public Renderer[] MainRenderers;


        private Animator _animator;
        private AnimationEvents _animationEvents;
        private HpBar _hpComponent;
        private Rigidbody _playerRb;
        private PlayerInputs _playerInputs;
        private AttackCollidersHandler _attackCollidersHandler;
        private DetectCollider _pickUpRange;
        private DetectCollider _rangeOfAttack;

        public Animator Animator => _animator ? _animator : _animator = GetComponentInChildren<Animator>();
        public AnimationEvents AnimationEvents => _animationEvents ? _animationEvents : _animationEvents = GetComponentInChildren<AnimationEvents>();
        public HpBar HpComponent => _hpComponent ? _hpComponent : _hpComponent = GetComponent<HpBar>();
        public Rigidbody PlayerRb => _playerRb ? _playerRb : _playerRb = GetComponent<Rigidbody>();
        public PlayerInputs PlayerInputs => _playerInputs ? _playerInputs : _playerInputs = GetComponent<PlayerInputs>();
        public AttackCollidersHandler AttackCollidersHandler => _attackCollidersHandler ? _attackCollidersHandler : _attackCollidersHandler = GetComponentInChildren<AttackCollidersHandler>();
        public DetectCollider PickUpRange => _pickUpRange ? _pickUpRange : _pickUpRange = transform.Find("Pick Up Range").GetComponent<DetectCollider>();
        public DetectCollider RangeOfAttack => _rangeOfAttack ? _rangeOfAttack : _rangeOfAttack = transform.Find("Range of Attack").GetComponent<DetectCollider>();

        public Action OnNoMoveInput;
        public Action OnEndstate;
        
        private void OnEnable()
        {
            PlayerInputs.OnPickupInput += CheckIfThereIsPickupable;
        }
        private void OnDisable()
        {
            PlayerInputs.OnPickupInput -= CheckIfThereIsPickupable;
        }

        public override void Awake()
        {
            base.Awake();
            AssignWeapon();
            
        }
        
        public override void Update()
        {
            CurrentState.StateUpdate();
            CalculateMoveInputs();
            SlopeHandler();

        }
        public override void FixedUpdate()
        {
            CurrentState.StateFixedUpdate();
            
        }
        public override void SetState(PlayerMachineData newState)
        {
            if (newState == null || !newState.IsUnlocked)
                return;

            CurrentState?.Discard();
            CurrentState = newState.Initialize(this);
            //Debug.Log("State is now " + CurrentState.Data.name);
            //soundUpdate();
        }
        

        #region SOUND EVENTS
        /*public void soundUpdate()
        {
            if (CurrentState.Data.name == "Player Attack 1")
            {
                WeaponSound.attackEvent1?.Invoke();
            }
            if (CurrentState.Data.name == "Player Attack 2")
            {
                WeaponSound.attackEvent2?.Invoke();
            }
            if (CurrentState.Data.name == "Player Attack 3")
            {
                WeaponSound.attackEvent3?.Invoke();
            }
            if (CurrentState.Data.name == "Player Spin Attack")
            {
                WeaponSound.attackEvent4?.Invoke();
            }
        }*/

        #endregion
        #region PLAYER MOVEMENT FUNCTIONS
        [SerializeField, Foldout("Movement")] public ControlBindings PCControls;

        public FixedJoystick MobileJoystick;

        Vector3 MoveVelocityInputs;
        Vector3 CamRelativeMoveVect;
        float HorizontalMove;
        float VerticalMove;
        Transform PlayerCamTransform => Camera.main.transform;
        void SimulateGravity() => PlayerRb.velocity += Vector3.up * -9.81f * Time.fixedDeltaTime;
        void SlopeHandler()
        {
            RaycastHit hit;
            if (Physics.Raycast(FeetRayStart.position, -Vector3.up, out hit,  NavigatableAreas))
            {
                if (hit.point.y == transform.position.y) return;
                PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0f, PlayerRb.velocity.z);
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
            else
            {
                SimulateGravity();
            }
            
        }
        void CalculateMoveInputs()
        {
            HorizontalMove = Mathf.Clamp(HorizontalMove, -1, 1);
            VerticalMove = Mathf.Clamp(VerticalMove, -1, 1);

            Vector3 camForward = PlayerCamTransform.forward;
            Vector3 camRight = PlayerCamTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 forwardRelativeMoveVect = HorizontalMove * camForward;
            Vector3 rightRelativeMoveVect = VerticalMove * camRight;

            CamRelativeMoveVect = forwardRelativeMoveVect + rightRelativeMoveVect ;

            MoveVelocityInputs = CamRelativeMoveVect.normalized  *  Time.fixedDeltaTime;

            switch (PlayerInputs.PlatformType)
            {
                case PlatformType.PC:
                    CalculatePCInputs();
                    break;
                case PlatformType.Mobile:
                    if(MobileJoystick != null) CalculateMobileInputs();
                    break;
            }

            if (VerticalMove == 0 && HorizontalMove == 0) OnNoMoveInput?.Invoke();
        }
        void CalculatePCInputs()
        {
            if (Input.GetKey(PCControls.ForwardKey))
            {
                HorizontalMove += 1;
            }
            if (Input.GetKey(PCControls.BackwardKey))
            {
                HorizontalMove -= 1;
            }
            if (Input.GetKey(PCControls.LeftKey))
            {
                VerticalMove -= 1;
            }
            if (Input.GetKey(PCControls.RightKey))
            {
                VerticalMove += 1;
            }

            if (!Input.GetKey(PCControls.ForwardKey) && !Input.GetKey(PCControls.BackwardKey)) HorizontalMove = 0;
            if (!Input.GetKey(PCControls.RightKey) && !Input.GetKey(PCControls.LeftKey)) VerticalMove = 0;

        }
        void CalculateMobileInputs()
        {
            HorizontalMove = MobileJoystick.Vertical;
            VerticalMove = MobileJoystick.Horizontal;
        }
        public void RotateTowardsMovement(float rotationSpeed, bool isInstant)
        {

            Vector3 MoveDirection = new Vector3(CamRelativeMoveVect.x, 0f, CamRelativeMoveVect.z).normalized;
            Quaternion ForwardDirection = CamRelativeMoveVect.magnitude > 0.1f ? Quaternion.LookRotation(MoveDirection, Vector3.up) : Quaternion.identity;
            if (!isInstant) transform.rotation = Quaternion.RotateTowards(transform.rotation, ForwardDirection,
                                rotationSpeed * Time.deltaTime * (CamRelativeMoveVect.magnitude != 0 ? 1 : 0));
            else transform.rotation = ForwardDirection;
        }

        public void MoveHorizontal(float speed) => PlayerRb.velocity = new Vector3(MoveVelocityInputs.x * speed, PlayerRb.velocity.y, MoveVelocityInputs.z * speed);
        #endregion
        #region ATTACK VARIABLES

        [SerializeField, Foldout("Combat")] public Transform WeaponHandPosition;
        [SerializeField, Foldout("Combat")] public GameObject WeaponOnHandGameObject;
        [SerializeField, Foldout("Combat")] public IWeapon WeaponOnHand;
        [SerializeField, Foldout("Combat")] public LayerMask ClickableArea;
        [SerializeField, Foldout("Combat")] public float CurrentWeaponDamage;
        [SerializeField, Foldout("Combat")] public float CurrentWeaponSequenceResetTimer;



        [HideInInspector] public float AttackSequence;
        Coroutine AttackSequenceCoroutine;
        IEnumerator AttackSequenceResetEnumerator(float sequenceResetTime)
        {
            if (AttackSequence > 3) AttackSequence = 0;
            yield return new WaitForSeconds(sequenceResetTime);
            AttackSequence = 0;
        }
        public void AttackSequenceReset(float sequenceResetTime)
        {
            if (AttackSequenceCoroutine != null) StopCoroutine(AttackSequenceCoroutine);
            AttackSequenceCoroutine = StartCoroutine(AttackSequenceResetEnumerator(sequenceResetTime));
        }

        public void AssignWeapon()
        {
            WeaponOnHand = WeaponHandPosition.GetComponentInChildren<IWeapon>();
            WeaponOnHandGameObject = WeaponOnHand.GetGameobject();

            CurrentWeaponDamage = WeaponOnHand == null ? 5f : WeaponOnHand.Damage;
            CurrentWeaponSequenceResetTimer = WeaponOnHand == null ? 0.75f : WeaponOnHand.SequenceResetTime;
        }
        public void FaceDirectionOfMousePos()
        {
            Ray MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(MouseRay, out RaycastHit rayCastHit, ClickableArea))
            {
                Vector3 DirectionToLookAt = new Vector3(rayCastHit.point.x, transform.position.y, rayCastHit.point.z);
                transform.rotation = Quaternion.LookRotation(DirectionToLookAt - transform.position, Vector3.up);
            }
        }
        public void FaceToNearestEnemy()
        {
            if (RangeOfAttack.NearestGameobject() == null) return;

            Transform NearestEnemy = RangeOfAttack.NearestGameobject().transform;
            Vector3 Direction = NearestEnemy.position- transform.position;
            transform.rotation = Quaternion.LookRotation(Direction, Vector3.up);
        }
        #endregion
        #region INTERACTIONS

        [SerializeField, Foldout("Pick-up Variables")] public Rigidbody ItemPickedUpRb;
        public Action OnPickupItem;
        public Action OnNoItemPickup; // Added so when there is something that you want to happen when there is no item to pickup.

        public void CheckIfThereIsPickupable()
        {
            GameObject ObjectToPickup = PickUpRange.NearestGameobject();
            if (ObjectToPickup != null && !CurrentState.Data.name.Contains("Item Hold")) OnPickupItem?.Invoke();
            
            else OnNoItemPickup?.Invoke();
        }


        #endregion
        #region CONNECTED VARIABLES
        [SerializeField, Foldout("Variables")] public PlayerStats PlayerStatsSCO;
        
        public void UpdatePlayerStatsSCO()
        {
            PlayerStatsSCO.PlayerCurrentHealth = HpComponent.CurrentHealth;
        }

        #endregion

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
using System;
using NaughtyAttributes;
using Player.Controls;
using Core;
using InterfaceAndInheritables;
using Player;
using AudioSoundEvents;
using UnityEngine.EventSystems;
namespace StateMachine.Player
{
    [RequireComponent(typeof(PlayerInputs))]
    public class PlayerMonoStateMachine : StateMachineHandler<PlayerMachineData, PlayerMachineFunctions>
    {
        [SerializeField, Foldout("Transform")] private Transform FeetStart;
        [SerializeField, Foldout("Transform")] private Transform FeetEnd;
        [SerializeField, Foldout("Transform")] public Transform ItemHoldPosition;
        [SerializeField] public LayerMask NavigatableAreas;
        public Renderer[] MainRenderers;
        float Gravity = -96.2361f;

        private Animator _animator;
        private AnimationEvents _animationEvents;
        private HpBar _hpComponent;
        private Rigidbody _playerRb;
        private PlayerInputs _playerInputs;
        private AttackCollidersHandler _attackCollidersHandler;
        private DetectCollider _pickUpRange;
        private DetectCollider _rangeOfAttack;
        private DetectCollider _interactableDetector;

        public Animator Animator => _animator ? 
            _animator : _animator = GetComponentInChildren<Animator>();
        public AnimationEvents AnimationEvents => _animationEvents ?
            _animationEvents : _animationEvents = GetComponentInChildren<AnimationEvents>();
        public HpBar HpComponent => _hpComponent ?
            _hpComponent : _hpComponent = GetComponent<HpBar>();
        public Rigidbody PlayerRb => _playerRb ?
            _playerRb : _playerRb = GetComponent<Rigidbody>();
        public PlayerInputs PlayerInputs => _playerInputs ? 
            _playerInputs : _playerInputs = GetComponent<PlayerInputs>();
        public AttackCollidersHandler AttackCollidersHandler => _attackCollidersHandler ? 
            _attackCollidersHandler : _attackCollidersHandler = GetComponentInChildren<AttackCollidersHandler>();
        public DetectCollider PickUpRange => _pickUpRange ? 
            _pickUpRange : _pickUpRange = transform.Find("Pick Up Range").GetComponent<DetectCollider>();
        public DetectCollider RangeOfAttack => _rangeOfAttack ? 
            _rangeOfAttack : _rangeOfAttack = transform.Find("Range of Attack").GetComponent<DetectCollider>();
        public DetectCollider InteractableDetector => _interactableDetector ? 
            _interactableDetector : _interactableDetector = transform.Find("Detect Interactable").GetComponent<DetectCollider>();

        public Action OnNoMoveInput;
        public Action OnEndstate;
        public Action OnFalling;
        public Action OnLanded;

        private void OnEnable()
        {
            PlayerInputs.OnPickupInput += CheckIfThereIsPickupable;
            PlayerInputs.OnInteractInput += InteractWithInteractable;
        }
        private void OnDisable()
        {
            PlayerInputs.OnPickupInput -= CheckIfThereIsPickupable;
            PlayerInputs.OnInteractInput -= InteractWithInteractable;
        }

        public override void Awake()
        {
            
            base.Awake();
            if (WeaponHolderPosition.childCount > 0) for (int i = 0; i < WeaponHolderPosition.childCount; i++) Destroy(WeaponHolderPosition.GetChild(0).gameObject); // Makes sure to destroy all weapons on hand on awake
            AssignWeaponAndOrShield();
            SetSpawnPosition();
        }
        public override void Update()
        {
            CurrentState.StateUpdate();
            SlopeHandler();
            CalculateMoveInputs();
            DetectIfFalling();

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
        
        #region PLAYER MOVEMENT FUNCTIONS
        [SerializeField, Foldout("Movement")] public ControlBindings PCControls;

        [HideInInspector] public FixedJoystick MobileJoystick;

        Vector3 MoveVelocityInputs;
        Vector3 CamRelativeMoveVect;
        float HorizontalMove;
        float VerticalMove;
        Transform PlayerCamTransform => Camera.main.transform;
        void DetectIfFalling()
        {
            if (PlayerRb.velocity.y < -0.5f)
            {
                OnFalling?.Invoke();
                Debug.Log("FALLING");
            }
        }
        void SimulateGravity() => PlayerRb.velocity += Vector3.up * Gravity * Time.deltaTime;

       
        void SlopeHandler()
        {
            RaycastHit hit;
            Ray ray = new Ray(FeetStart.position, Vector3.down);

            float sphereRadius = (FeetStart.position.y  - FeetEnd.position.y) /2 ; 
            float sphereMaxDist = sphereRadius * 4f; 
            
            Debug.DrawRay(FeetStart.position, Vector3.down * 0.2f, Color.red);



            //  OLD LINE. BRING BACK IF THERE IS ISSUE WITH SPHERECAST 
            // Physics.SphereCast(FeetStart.position, sphereRadius, Vector3.down, out hit, sphereMaxDist, NavigatableAreas) SPHERECAST
            // Debug.Log("SPHERE RADIUS: " + sphereRadius + "  SPHERE MAX DIST: " + sphereMaxDist + "    PLAYER VELOCITY: " + PlayerRb.velocity.y);
            if (Physics.Raycast(ray, out hit, 1f, NavigatableAreas))
            {
                //Debug.Log("SPHERECAST HITS SOMETHING " + hit.transform.name);
                GoToHitPoint(hit);
            }
            else if(Physics.Raycast(ray, out hit, 0.2f, NavigatableAreas)) // This is if the spherecast fails, which it sometimes does
            {
                //Debug.Log("CAST2 HITS SOMETHING " + hit.transform.name);
                GoToHitPoint(hit);
            }
            else
            {
                SimulateGravity();
            }
            
        }

        void GoToHitPoint(RaycastHit hit)
        {
            if (PlayerRb.velocity.y > 0.1f) return;

            float HitPointYRounded = Mathf.Round(hit.point.y * 10.0f) * 0.1f;
            float PositionYRounded = Mathf.Round(transform.position.y * 10.0f) * 0.1f;// Round off both to compare at a not TOO accurate value

            //Debug.Log("HITY: " + HitPointYRounded + "    POSY: " + PositionYRounded);
            if (HitPointYRounded == PositionYRounded) return; // This line is bugged. makes me angy >:[
            Debug.Log(HitPointYRounded == PositionYRounded); // Had to add this to fix the line above. If you try to remove this, you will experience an issue with jumping
            PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0f, PlayerRb.velocity.z);
            Debug.Log("LANDED");
            SetPosition(new Vector3(transform.position.x, hit.point.y, transform.position.z));
            OnLanded?.Invoke();
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
            if (Input.GetKey(PCControls.ForwardKey)) HorizontalMove += 1;
            if (Input.GetKey(PCControls.BackwardKey)) HorizontalMove -= 1;
            if (Input.GetKey(PCControls.LeftKey)) VerticalMove -= 1;
            if (Input.GetKey(PCControls.RightKey))VerticalMove += 1;
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
            Quaternion ForwardDirection = CamRelativeMoveVect.magnitude > 0.1f ? 
                Quaternion.LookRotation(MoveDirection, Vector3.up) : Quaternion.identity;
            if (!isInstant) transform.rotation = Quaternion.RotateTowards(transform.rotation, ForwardDirection,
                                rotationSpeed * Time.deltaTime * (CamRelativeMoveVect.magnitude != 0 ? 1 : 0));
            else transform.rotation = ForwardDirection;
        }
        public void MoveHorizontal(float speed) => PlayerRb.velocity = new Vector3(MoveVelocityInputs.x * speed, PlayerRb.velocity.y, MoveVelocityInputs.z * speed);
        public void StopMovement() => PlayerRb.velocity = Vector3.zero;
        public void SetPosition(Vector3 newPosition) => gameObject.transform.position = newPosition;
        public void SetSpawnPosition()
        {
            RaycastHit hit;
            Ray ray = new Ray(FeetStart.position, Vector3.down);
            Physics.Raycast(ray, out hit, Mathf.Infinity, NavigatableAreas);
            SetPosition(new Vector3(transform.position.x, hit.point.y, transform.position.z));
        }

        #endregion
        #region ATTACK

        [SerializeField, Foldout("Combat")] public Transform WeaponHolderPosition;
        [SerializeField, Foldout("Combat")] public Transform ShieldHolderPosition;
        [SerializeField, Foldout("Combat")] public GameObject WeaponOnHandGameObject;
        [SerializeField, Foldout("Combat")] public IWeapon WeaponOnHand;
        [SerializeField, Foldout("Combat")] public LayerMask ClickableArea;
        [SerializeField, Foldout("Combat")] public float CurrentWeaponDamage;
        [SerializeField, Foldout("Combat")] public DamageType CurrentWeaponDamageType;
        [SerializeField, Foldout("Combat")] public float CurrentWeaponSequenceResetTimer;
        [HideInInspector] public Collider ShieldCollider;
         public float AttackSequence;
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

        public void AssignWeaponAndOrShield()
        {
            
            WeaponOnHand = null;
            WeaponOnHandGameObject = null;
            WeaponOnHand = WeaponHolderPosition?.GetComponentInChildren<IWeapon>();
            ShieldCollider = ShieldHolderPosition?.GetComponentInChildren<Collider>();
            WeaponOnHandGameObject = WeaponOnHand?.GetGameobject();
            if (WeaponOnHand != null)
            {
                Debug.Log("ASSIGN WEAPON CALLED WEAPON ASSIGNED IS " + WeaponOnHand.GetGameobject());
                WeaponOnHand.holder = this;
                WeaponOnHand.SubscribeEvents();
            }

            CurrentWeaponDamage = WeaponOnHand == null ? 5f : WeaponOnHand.Damage;
            CurrentWeaponDamageType = WeaponOnHand == null ? 
                DamageType.MELEE : WeaponHolderPosition.GetComponentInChildren<IWeapon>().WeaponDamageType;
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
            Vector3 PositionToFace = new Vector3(NearestEnemy.position.x, transform.position.y, NearestEnemy.position.z);
            Vector3 Direction = PositionToFace - transform.position;
            transform.rotation = Quaternion.LookRotation(Direction, Vector3.up);
        }
        #endregion
        #region INTERACTIONS
        [SerializeField, Foldout("Pick-up Variables")] public Rigidbody ItemPickedUpRb;
        [SerializeField, Foldout("Pick-up Variables")] public float MaxHoldDistance;
        [SerializeField, Foldout("Pick-up Variables")] public bool PlayerIsHoldingObject = false;
        public Action OnPickupItem;
        public Action OnNoItemPickup;
        public void CheckIfThereIsPickupable()
        {
            GameObject ObjectToPickup = PickUpRange.NearestGameobject();
            if (ObjectToPickup != null && !CurrentState.Data.name.Contains("Item Hold")) OnPickupItem?.Invoke();
            else OnNoItemPickup?.Invoke();
        }
        public void InteractWithInteractable()
        {
            if(IsThereInteractableInRange())
            {
                GameObject InteractedObject = InteractableDetector.NearestGameobject();
                InteractedObject.GetComponent<Interactable>()?.Interact(this);
                InteractableDetector.ObjectsThatIsInRange.Remove(InteractedObject);
            }
        }
        bool IsThereInteractableInRange()
        {
            if (InteractableDetector.ObjectsThatIsInRange.Count > 0) return true;
            else return false;
        }

        #endregion
        #region CONNECTED VARIABLES
        [SerializeField, Foldout("Variables")] public PlayerStats PlayerStatsSCO;
        #endregion

    }
}

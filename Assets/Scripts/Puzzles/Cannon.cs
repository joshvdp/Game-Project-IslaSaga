using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.Events;

namespace Puzzle
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] Animator CannonAnimator;
        AnimationEvents CannonAnimEvents => CannonAnimator.gameObject.GetComponent<AnimationEvents>();
        [SerializeField] GameObject ProjectileGameobject;
        [SerializeField] Transform FireOrigin;
        [SerializeField] float DefaultFireSpeed;
        [SerializeField] float ProjectileSpeed;
        [SerializeField] float StartFireDelay;
        float FireSpeed;
        bool CanFire = false;
        public UnityEvent ShotsFired;

        private void OnEnable()
        {
            CannonAnimEvents.FindEvent("On Cannon Fire").AddListener(ResetFireInterval);
            CannonAnimEvents.FindEvent("On Cannon Fire").AddListener(Fire);
        }

        private void OnDisable()
        {
            CannonAnimEvents.FindEvent("On Cannon Fire").RemoveListener(ResetFireInterval);
            CannonAnimEvents.FindEvent("On Cannon Fire").RemoveListener(Fire);
        }
        private void Awake()
        {
            FireSpeed = DefaultFireSpeed;
            Invoke("ResetFireInterval", StartFireDelay);
        }

        private void Update()
        {
            if (FireSpeed > 0) FireSpeed -= Time.deltaTime;
            else if(CanFire)
            {
                CannonAnimator.SetTrigger("Fire");
                CanFire = false;
            }
        }

        void Fire()
        {
            ShotsFired?.Invoke();
            Rigidbody ProjectileRb = Instantiate(ProjectileGameobject, FireOrigin.position, Quaternion.identity).GetComponent<Rigidbody>();
            ProjectileRb.velocity = transform.right * ProjectileSpeed;
        }
        void ResetFireInterval()
        {
            FireSpeed = DefaultFireSpeed;
            CanFire = true;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.Events;

namespace Puzzle
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] GameObject ProjectileGameobject;
        [SerializeField] Transform FireOrigin;
        [SerializeField] float DefaultFireSpeed;
        [SerializeField] float ProjectileSpeed;
        float FireSpeed;
        public UnityEvent ShotsFired;

        private void Awake()
        {
            FireSpeed = DefaultFireSpeed;
        }

        private void Update()
        {
            if (FireSpeed > 0)FireSpeed -= Time.deltaTime;
            else
            {
                Fire();
                FireSpeed = DefaultFireSpeed;
            }
        }

        void Fire()
        {
            ShotsFired?.Invoke();
            Rigidbody ProjectileRb = Instantiate(ProjectileGameobject, FireOrigin.position, Quaternion.identity).GetComponent<Rigidbody>();
            ProjectileRb.velocity = transform.right * ProjectileSpeed;
        }
        
        

    }
}

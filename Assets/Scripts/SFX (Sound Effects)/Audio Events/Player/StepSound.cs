using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioSoundEvents
{
    public class StepSound : MonoBehaviour
    {
        public delegate void stepEvent();
        public static stepEvent walk, run;

        public GameObject Walking, Running;

        private FootStep _step;
        
        private void OnEnable()
        {
            walk += Walk;
            run += Run;
            
        }
        private void OnDisable()
        {
            walk -= Walk;
            run -= Run;
        }
        private void Start()
        {
            _step = GetComponent<FootStep>();
        }
        private void Walk()
        {
            //Debug.Log("Im Walking");
            GameObject _step = Instantiate(Walking, transform.position, transform.rotation);
        }
        private void Run()
        {
            Debug.Log("Im Running");
            //GameObject _step = Instantiate(Running, transform.position, transform.rotation);
        }
    }
}


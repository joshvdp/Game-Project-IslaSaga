using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Puzzle_PC : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject lastDia;


        RectTransform bg;

        private void Start()
        {
            
            
            bg = gameObject.GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            TouchField.OnTouchDown += DoThisOnDown;
        }

        private void OnDisable()
        {
            TouchField.OnTouchDown -= DoThisOnDown;
        }

        void DoThisOnDown()
        {
            

            

            if (lastDia.activeSelf)
            {
                
                
            }

        }
    }
}


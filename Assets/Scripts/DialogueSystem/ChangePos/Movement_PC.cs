using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Movement_PC : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject scndIntro, lastIntro, atk, run, blk, jmp, lastDia,      //dialogue
                           map, health;  // buttons & UI

        RectTransform bg;

        private void Start()
        {
            bg = gameObject.GetComponent<RectTransform>();
            
            
        }

        

        void Update()
        {
            if (scndIntro.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    map.SetActive(false);
                    health.SetActive(false);
                }

                

            }

           

            if (lastDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    map.SetActive(true);
                    health.SetActive(true);
                }
                
                
            }
        }
    }
}


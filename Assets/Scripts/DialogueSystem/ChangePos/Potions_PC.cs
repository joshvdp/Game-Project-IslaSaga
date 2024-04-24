using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Potions_PC : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject lastDia,      //dialogue
                          map, health;  // buttons & UI

        RectTransform bg;

        private void Start()
        {
            bg = gameObject.GetComponent<RectTransform>();
            
            map.SetActive(false);
            health.SetActive(false);
        }

        

        void Update()
        {

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


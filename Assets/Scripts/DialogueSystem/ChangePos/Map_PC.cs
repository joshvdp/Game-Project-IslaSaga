using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Map_PC : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject hp, lastDia,      //dialogue
                          map, healthBar;  // map & buttons

        RectTransform bg;

        private void Start()
        {
            bg = gameObject.GetComponent<RectTransform>();
            healthBar.SetActive(false);
            
        }

        

        void Update()
        {
            if (hp.activeSelf)
            {
                bg.transform.localPosition = new Vector3(46, 422, 0f);
                healthBar.SetActive(true);
                map.SetActive(false);
                if (Input.anyKeyDown)
                {
                    
                    
                }
                
            }

            if (lastDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    map.SetActive(true);
                }
                
                
            }
        }
    }
}


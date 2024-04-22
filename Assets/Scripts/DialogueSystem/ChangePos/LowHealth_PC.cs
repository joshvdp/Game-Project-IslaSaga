using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class LowHealth_PC : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject scndIntro, thirdDia, lastDia,      //dialogue
                          map;  // buttons & UI

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
                    bg.transform.localPosition = new Vector3(42, 409, 0f);
                    map.SetActive(false);
                }
                


            }

            if (thirdDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    bg.transform.localPosition = new Vector3(0, 137.05f, 0f);
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


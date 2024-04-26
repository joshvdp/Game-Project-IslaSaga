using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Inventory_PC : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject firstDia, secondDia, thirdDia, forthDia, fifthDia, sixthDia, lastDia,   //dialogue
                          map, healthBar, inventorySlots, tabs, invenTutorial; //UI
             

        RectTransform bg;

        private void Start()
        {
            invenTutorial.SetActive(true);
            bg = gameObject.GetComponent<RectTransform>();
        }

        

        void Update()
        {
            if (firstDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    bg.transform.localPosition = new Vector3(-392, 137.05f, 0f);
                    tabs.SetActive(false);
                }
                
                
            }

            if (secondDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    bg.transform.localPosition = new Vector3(-123, 294, 0f);
                    inventorySlots.SetActive(false);
                    tabs.SetActive(true);
                }
                
            }

            if (thirdDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    bg.transform.localPosition = new Vector3(-334, 66, 0f);
                }
                

            }

            if (forthDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    bg.transform.localPosition = new Vector3(-392, 137.05f, 0f);

                    inventorySlots.SetActive(true);
                }
                
            }

            if (fifthDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    bg.transform.localPosition = new Vector3(-334, 66, 0f);
                }
                

            }

            if (sixthDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    bg.transform.localPosition = new Vector3(3.0518e-05f, 137.05f, 0f);
                }
                
            }

            if (lastDia.activeSelf)
            {
                if (Input.anyKeyDown)
                {
                    map.SetActive(true);
                    healthBar.SetActive(true);

                    invenTutorial.SetActive(false);
                }
                
            }
        }
    }
}


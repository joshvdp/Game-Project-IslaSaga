using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Inventory : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject firstDia, secondDia, thirdDia, forthDia, fifthDia, sixthDia, lastDia,   //dialogue
                          map, healthBar, mobileUI, exitInventory, inventory, inventorySlots, tabs, analog, buttons, pause, Mobileinventorybtn, invenTutorial, //UI
                            sampleInventoryBtn;  // button

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
            if (firstDia.activeSelf)
            {
                Destroy(sampleInventoryBtn);
                bg.transform.localPosition = new Vector3(-392, 137.05f, 0f);
                exitInventory.SetActive(false);
                tabs.SetActive(false);
            }

            if (secondDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(-123, 294, 0f);
                inventorySlots.SetActive(false);
                tabs.SetActive(true);
            }

            if (thirdDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(-334, 66, 0f);
                
            }

            if (forthDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(-392, 137.05f, 0f);
                
                inventorySlots.SetActive(true);
            }

            if (fifthDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(-334, 66, 0f);
                
            }

            if (sixthDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(3.0518e-05f, 137.05f, 0f);
            }

            if (lastDia.activeSelf)
            {
                map.SetActive(true);
                healthBar.SetActive(true);
                buttons.SetActive(true);
                Mobileinventorybtn.SetActive(true);
                pause.SetActive(true);
                analog.SetActive(true);
                exitInventory.SetActive(true);
                inventory.SetActive(false);
                Destroy(invenTutorial);

            }
        }
    }
}


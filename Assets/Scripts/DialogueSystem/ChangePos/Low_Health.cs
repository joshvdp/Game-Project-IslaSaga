using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Low_Health : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject scndIntro, thirdDia, lastDia,      //dialogue
                          sprint, block, jump, attack, inventory, analog, map, pause;  // buttons & UI

        RectTransform bg;

        private void Start()
        {
            bg = gameObject.GetComponent<RectTransform>();
            pause.SetActive(false);
            sprint.SetActive(false);
            block.SetActive(false);
            jump.SetActive(false);
            attack.SetActive(false);
            inventory.SetActive(false);
            analog.SetActive(false);
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
            if (scndIntro.activeSelf)
            {
                bg.transform.localPosition = new Vector3(42, 409, 0f);
                map.SetActive(false);


            }

            if (thirdDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(0, 137.05f, 0f);
            }

            if (lastDia.activeSelf)
            {
                sprint.SetActive(true);
                block.SetActive(true);
                jump.SetActive(true);
                attack.SetActive(true);
                inventory.SetActive(true);
                analog.SetActive(true);
                map.SetActive(true);
            }
        }
    }
}


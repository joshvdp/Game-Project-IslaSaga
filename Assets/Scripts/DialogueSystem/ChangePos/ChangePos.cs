using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class ChangePos : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject scndIntro, lastIntro, atk, run, blk, jmp, lastDia,      //dialogue
                          sprint, block, pickup, jump, attack, inventory, analog, map, health;  // buttons & UI

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
            if (scndIntro.activeSelf)
            {
                bg.transform.localPosition = new Vector3(-353, 137.05f, 0f);
                sprint.SetActive(false);
                block.SetActive(false);
                pickup.SetActive(false);
                jump.SetActive(false);
                attack.SetActive(false);
                inventory.SetActive(false);
                analog.SetActive(false);
                map.SetActive(false);
                health.SetActive(false);

            }

            if (lastIntro.activeSelf)
            {
                bg.transform.localPosition = new Vector3(-303, -103, 0f);
                analog.SetActive(true);
            }


            if (atk.activeSelf)
            {
                bg.transform.localPosition = new Vector3(355, -143, 0f);
                attack.SetActive(true);
            }

            if (run.activeSelf)
            {
                bg.transform.localPosition = new Vector3(128, -178, 0f);
                sprint.SetActive(true);
            }

            if (blk.activeSelf)
            {
                bg.transform.localPosition = new Vector3(173, -34, 0f);
                block.SetActive(true);
            }

            if (jmp.activeSelf)
            {
                bg.transform.localPosition = new Vector3(485, 153.02f, 0f);
                jump.SetActive(true);
                

            }

            if (lastDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(0, 137.05f, 0f);
                map.SetActive(true);
                health.SetActive(true);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Puzzles : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject firstDia, thirdDia, lastDia,
                          atk, analog, sprint, block, jump, pickup, inventory, pause;


        RectTransform bg;

        private void Start()
        {
            pause.SetActive(false);
            atk.SetActive(false);
            sprint.SetActive(false);
            block.SetActive(false);
            jump.SetActive(false);
            analog.SetActive(false);
            inventory.SetActive(false);
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
                bg.transform.localPosition = new Vector3(290, 111.86f, 0f);
                pickup.SetActive(true);
            }

            if (thirdDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(0, 137.05f, 0f);
            }

            if (lastDia.activeSelf)
            {
                atk.SetActive(true);
                sprint.SetActive(true);
                block.SetActive(true);
                jump.SetActive(true);
                analog.SetActive(true);
                pause.SetActive(true);
            }

        }
    }
}


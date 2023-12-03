using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Potions : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject secondDia, lastDia,      //dialogue
                          buttons, analog, map, health, potion;  // buttons & UI

        RectTransform bg;

        private void Start()
        {
            bg = gameObject.GetComponent<RectTransform>();
            analog.SetActive(false);
            buttons.SetActive(false);
            map.SetActive(false);
            health.SetActive(false);
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
            if (secondDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(4.5776e-05f, 0f, 0f);
                potion.SetActive(true);
            }


            if (lastDia.activeSelf)
            {
                analog.SetActive(true);
                buttons.SetActive(true);
                map.SetActive(true);
                health.SetActive(true);
            }
        }
    }
}



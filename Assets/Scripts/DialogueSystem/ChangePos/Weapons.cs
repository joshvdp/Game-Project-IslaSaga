using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Weapons : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject lastDia,      //dialogue
                          buttons, analog, map, health;  // buttons & UI

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


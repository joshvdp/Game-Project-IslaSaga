using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Map_Hp : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject hp, lastDia,      //dialogue
                          map, healthBar;  // map

        RectTransform bg;

        private void Start()
        {
            bg = gameObject.GetComponent<RectTransform>();
            healthBar.SetActive(false);
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
            if (hp.activeSelf)
            {
                bg.transform.localPosition = new Vector3(46, 422, 0f);
                healthBar.SetActive(true);
                map.SetActive(false);
            }

            if (lastDia.activeSelf)
            {
                map.SetActive(true);
            }
        }
    }
}




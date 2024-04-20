using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChangePosition
{
    public class Movement_PC : MonoBehaviour
    {
        public FixedTouchField TouchField;

        public GameObject scndIntro, lastIntro, atk, run, blk, jmp, lastDia,      //dialogue
                           map, health, pause;  // buttons & UI

        RectTransform bg;

        private void Start()
        {
            bg = gameObject.GetComponent<RectTransform>();
            pause.SetActive(false);
            
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

                map.SetActive(false);
                health.SetActive(false);

            }

            if (lastIntro.activeSelf)
            {
                bg.transform.localPosition = new Vector3(-303, -103, 0f);
                
            }


            if (atk.activeSelf)
            {
                bg.transform.localPosition = new Vector3(355, -143, 0f);

            }

            if (run.activeSelf)
            {
                bg.transform.localPosition = new Vector3(128, -178, 0f);

            }

            if (blk.activeSelf)
            {
                bg.transform.localPosition = new Vector3(173, -34, 0f);

            }

            if (jmp.activeSelf)
            {
                bg.transform.localPosition = new Vector3(485, 153.02f, 0f);



            }

            if (lastDia.activeSelf)
            {
                bg.transform.localPosition = new Vector3(0, 137.05f, 0f);
                map.SetActive(true);
                pause.SetActive(true);
            }
        }
    }
}


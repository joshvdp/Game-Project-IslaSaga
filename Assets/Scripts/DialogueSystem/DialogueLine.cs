using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        public FixedTouchField TouchField;
        public UnityEvent OnFinished;
        
        [SerializeField]private string input;
        private TMP_Text textHolder;

        [SerializeField] private float delay;


        private IEnumerator lineAppear;

        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();
            textHolder.text = "";
        }

        private void OnEnable()
        {
            TouchField.OnTouchDown += DoThisOnDown;

            ResetLine();
            lineAppear = WriteText(input, textHolder, delay);
            StartCoroutine(lineAppear);
        }

        private void OnDisable()
        {
            TouchField.OnTouchDown -= DoThisOnDown;
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else
                {
                    Finished = true;
                    OnFinished?.Invoke();
                }
            }
        }

        private void ResetLine()
        {
            textHolder = GetComponent<TMP_Text>();
            textHolder.text = "";
            Finished = false;
        }

        void DoThisOnDown()
        {
            if (textHolder.text != input)
            {
                StopCoroutine(lineAppear);
                textHolder.text = input;
            }
            else
                Finished = true;
        }
    }

}


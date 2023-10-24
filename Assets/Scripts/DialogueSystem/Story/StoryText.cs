using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Story
{
    public class StoryText : MonoBehaviour
    {
        float textSpeed = 100.0f;
        float textBeginPosition = -1064f;  
        float textEndPosition = 1068f;

        public GameObject Skip;

        RectTransform myGorectTransform;

        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] bool isLooping = false;
        private void Start()
        {
            myGorectTransform = gameObject.GetComponent<RectTransform>();
            StartCoroutine(AutoScrollText());
            StartCoroutine(ShowSkipButton());
        }

        IEnumerator AutoScrollText()
        {
            while (myGorectTransform.localPosition.y < textEndPosition)
            {
                myGorectTransform.Translate(Vector3.up * textSpeed * Time.deltaTime);
                if (myGorectTransform.localPosition.y > textEndPosition)
                {
                    if (isLooping)
                    {
                        myGorectTransform.localPosition = Vector3.up * textBeginPosition;
                    }
                    else
                    {
                        SceneManager.LoadScene(1);
                        break;
                    }
                }
                yield return null;
            }
            
        }

        

        IEnumerator ShowSkipButton()
        {
            yield return new WaitForSeconds(5);

            
            Skip.SetActive(true);

            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine(AutoScrollText());
                SceneManager.LoadScene(1);
            }
        }





    }
}



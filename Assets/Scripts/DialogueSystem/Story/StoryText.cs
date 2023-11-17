using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Story
{
    public enum UIPlatformType
    {
        PC,
        Mobile
    }
    public class StoryText : MonoBehaviour
    {
        public UIPlatformType PlatformType;

        float textSpeed = 38.0f;
        float textBeginPosition = -1064f;  
        float textEndPosition = 1347f;

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
            switch (PlatformType)
                {
                    case UIPlatformType.Mobile:
                        textSpeed = 45.0f;
                        myGorectTransform.transform.localPosition = new Vector3(0f, -1231f, 0f);
                        textEndPosition = 1232f;
                        break;
                }
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

            yield return new WaitForSeconds(25);


            Skip.SetActive(false);


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



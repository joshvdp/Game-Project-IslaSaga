using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
using Manager;
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

        float textSpeed;
        float textBeginPosition;  
        float textEndPosition;

        public GameObject Skip;

        RectTransform myGorectTransform;

        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] bool isLooping = false;
        private void Start()
        {
            string activeSceneName = SceneManager.GetActiveScene().name;
            myGorectTransform = gameObject.GetComponent<RectTransform>();
            
            
            switch (activeSceneName)
            {
                case "StoryScene":
                    StartCoroutine(Story());
                    break;
                case "AfterLevel":
                    StartCoroutine(AfterLevel());
                    break;
                case "EndScene":
                    StartCoroutine(End());
                    break;
            }

            StartCoroutine(ShowSkipButton());
        }

        IEnumerator Story()
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
                        SceneLoader.Instance.LoadNextSceneAsync(SceneLoader.Instance.SceneName.Level1);
                        break;
                    }
                }
                yield return null;
            }

            
        }

        IEnumerator AfterLevel()
        {
            switch (PlatformType)
            {
                case UIPlatformType.Mobile:
                    textSpeed = 100;
                    myGorectTransform.transform.localPosition = new Vector3(0f, -618, 0f);
                    textEndPosition = 604f;
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
                        SceneLoader.Instance.LoadNextSceneAsync(SceneLoader.Instance.SceneName.Level1);
                        break;
                    }
                }
                yield return null;
            }


        }

        IEnumerator End()
        {
            switch (PlatformType)
            {
                case UIPlatformType.Mobile:
                    textSpeed = 45.0f;
                    myGorectTransform.transform.localPosition = new Vector3(0f, -710, 0f);
                    textEndPosition = 697f;
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
                        SceneLoader.Instance.LoadNextSceneAsync(SceneLoader.Instance.SceneName.Level1);
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
            string activeSceneName = SceneManager.GetActiveScene().name;
            if (Input.GetMouseButtonDown(0))
            {

                switch (activeSceneName)
                {
                    case "StoryScene":
                        StopCoroutine(Story());
                        break;
                    case "AfterLevel":
                        StopCoroutine(AfterLevel());
                        break;
                    case "EndScene":
                        StopCoroutine(End());
                        break;
                        
                }

                SceneLoader.Instance.LoadNextSceneAsync(SceneLoader.Instance.SceneName.Level1);
                Debug.Log("CLICK DETECTED");
            }
        }





    }
}



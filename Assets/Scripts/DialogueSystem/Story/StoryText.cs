using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Video;
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

        float Speed;
        float BeginPosition;  
        float LastPosition;

        public VideoPlayer videoPlayer;

        public GameObject Skip;

        RectTransform myGorectTransform;

        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] bool isLooping = false;
        [SerializeField] string NextSceneName;

        [Header("For PC")]
        [SerializeField] float TextSpeed;
        [SerializeField] Vector3 StartingPostion;
        [SerializeField] float EndPosition;

        [Header("For Mobile")]
        [SerializeField] float textSpeed;
        [SerializeField] Vector3 textBeginPosition;
        [SerializeField] float textEndPosition;
        private void Start()
        {
            string activeSceneName = SceneManager.GetActiveScene().name;
            myGorectTransform = gameObject.GetComponent<RectTransform>();

            //Debug.Log("ACTIVE SCENE NAME IS " + activeSceneName);


            StartCoroutine(Story());
            StartCoroutine(ShowSkipButton());
            StartCoroutine(PlayVideo());
        }

        IEnumerator Story()
        {
            switch (PlatformType)
                {
                    case UIPlatformType.Mobile:
                        Speed = textSpeed;
                        myGorectTransform.transform.localPosition = textBeginPosition;
                        LastPosition = textEndPosition;
                        break;

                    case UIPlatformType.PC:
                        Speed = TextSpeed;
                        myGorectTransform.transform.localPosition = StartingPostion;
                        LastPosition = EndPosition;
                        break;
                }
            while (myGorectTransform.localPosition.y < LastPosition)
            {
                
                myGorectTransform.Translate(Vector3.up * Speed * Time.deltaTime);
                if (myGorectTransform.localPosition.y > LastPosition)
                {
                    if (isLooping)
                    {
                        
                    }
                    else
                    {
                        SceneLoader.Instance.LoadNextSceneAsync(NextSceneName); 
                        break;
                    }
                }
                yield return null;
            }

            
        }



        IEnumerator PlayVideo()
        {
            yield return new WaitForSeconds(50);

            videoPlayer.Play();
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
            
            
            
            if (Input.GetMouseButtonDown(0) && !SceneLoader.Instance.IsLoadingScreenPresent)
            {

                

                SceneLoader.Instance.LoadNextSceneAsync(NextSceneName);
            }
        }

        void ScrollText()
        {
            if(myGorectTransform.localPosition.y < textEndPosition)
            {

                myGorectTransform.Translate(Vector3.up * TextSpeed * Time.deltaTime);
                if (myGorectTransform.localPosition.y > textEndPosition)
                {
                    if (isLooping)
                    {
                        
                    }
                    else
                    {
                        SceneLoader.Instance.LoadNextSceneAsync(NextSceneName);
                        
                    }
                }
                
            }
        }



    }
}



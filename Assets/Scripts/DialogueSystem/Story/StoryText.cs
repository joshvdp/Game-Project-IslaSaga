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
        float textBeginPosition = -952f;  
        float textEndPosition = 962f;

        RectTransform myGorectTransform;

        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] bool isLooping = false;
        private void Start()
        {
            myGorectTransform = gameObject.GetComponent<RectTransform>();
            StartCoroutine(AutoScrollText());
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

        


    }
}



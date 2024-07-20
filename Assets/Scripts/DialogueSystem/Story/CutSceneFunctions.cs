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
    public class CutSceneFunctions : MonoBehaviour
    {

        [SerializeField] VideoPlayer videoPlayer;

        [SerializeField] UnityEvent OnTapOrClick;
        [SerializeField] UnityEvent OnVideoReachedEnd;

        private void OnEnable()
        {
            if(videoPlayer != null) videoPlayer.loopPointReached += EndOfVideo;
        }

        private void OnDisable()
        {
            if (videoPlayer != null) videoPlayer.loopPointReached -= EndOfVideo;
        }
        void EndOfVideo(VideoPlayer vp)
        {
            OnVideoReachedEnd?.Invoke();
        }
        private void Update()
        {
            ListenToClickOrTap();
        }

        void ListenToClickOrTap()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnTapOrClick?.Invoke();
            }
        }
    }
}



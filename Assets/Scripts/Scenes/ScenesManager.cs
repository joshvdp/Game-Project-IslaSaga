using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Manager
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField] Animator transitionAnim;
        public void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void LoadScene()
        {
            StartCoroutine(loadNow());
        }
        IEnumerator loadNow()
        {
            transitionAnim.SetTrigger("End");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            transitionAnim.SetTrigger("Start");
        }

        public void exit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}


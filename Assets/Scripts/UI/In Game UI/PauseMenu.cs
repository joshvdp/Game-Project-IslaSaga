using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject pauseMenuUI, optionsMenuUI, dialogueBox;  //healthBar, miniMap, pauseButton;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        /*healthBar.SetActive(true);
        miniMap.SetActive(true);
        pauseButton.SetActive(true);*/
        MainManager.Instance.IsPaused = false;
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        Debug.Log("Resume");
    }

    public void Pause()
    {
        /*healthBar.SetActive(false);
        miniMap.SetActive(false);
        pauseButton.SetActive(false);*/
        MainManager.Instance.IsPaused = true;
        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        Time.timeScale = 0f;
        GameisPaused = true;
        Debug.Log("Paused");
    }

    public void exit2Menu()
    {
        Debug.Log("Exit to Menu");
    }

    
}

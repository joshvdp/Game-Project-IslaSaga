using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public enum PausePlatformType
{
    PC,
    Mobile
}
public class PauseMenu : MonoBehaviour
{
    public PausePlatformType PlatformType;

    public static bool GameisPaused = false;

    public GameObject pauseMenuUI, optionsMenuUI, dialogueBox, mobileUI, mobilePauseButton;  //healthBar, miniMap, pauseButton;

    public Button pauseButton;

    void Awake()
    {
        checkPlatform();
    }
    void Update()
    {
        UI();
    }


    private void checkPlatform()
    {
        switch (PlatformType)
        {
            case PausePlatformType.Mobile:
                mobileUI.SetActive(true);
                mobilePauseButton.SetActive(true);
                break;
        }
    }
    private void UI()
    {
        switch (PlatformType)
        {
            case PausePlatformType.PC:
                interfacePC();
                break;
            case PausePlatformType.Mobile:
                interfaceMobile();
                break;
        }
    }

    public void ResumeButton()
    {
        switch (PlatformType)
        {
            case PausePlatformType.PC:
                MainManager.Instance.IsPaused = false;

                pauseMenuUI.SetActive(false);

                Time.timeScale = 1f;

                Debug.Log("Resume");
                GameisPaused = false;
                break;
            case PausePlatformType.Mobile:
                MainManager.Instance.IsPaused = false;

                pauseMenuUI.SetActive(false);
                mobileUI.SetActive(true);
                mobilePauseButton.SetActive(true);

                Time.timeScale = 1f;

                Debug.Log("Resume");
                GameisPaused = false;
                break;
        }
        
    }

    public void PauseButton()
    {
        MainManager.Instance.IsPaused = true;

        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        mobileUI.SetActive(false);
        mobilePauseButton.SetActive(false);

        Time.timeScale = 0f;

        GameisPaused = true;
        Debug.Log("Paused");
    }

    #region PC Pause Menu
    private void interfacePC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                pcResume();

            }
            else
            {
                pcPause();
            }
        }
    }

    public void pcResume()
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

    public void pcPause()
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
    #endregion


    #region Mobile Pause Menu

    private void interfaceMobile()
    {
        pauseButton.onClick.AddListener(inputMobile); 
    }

    public void inputMobile()
    {
        if (GameisPaused)
        {
            mobileResume();
        }
        else
        {
            mobilePause();
        }
    }
    public void mobileResume()
    {
        
        MainManager.Instance.IsPaused = false;

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        mobileUI.SetActive(true);
        mobilePauseButton.SetActive(true);

        Time.timeScale = 1f;

        GameisPaused = false;
        Debug.Log("Resume");
    }

    public void mobilePause()
    {
        
        MainManager.Instance.IsPaused = true;

        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        mobileUI.SetActive(false);
        mobilePauseButton.SetActive(false);

        Time.timeScale = 0f;

        GameisPaused = true;
        Debug.Log("Paused");
    }

    #endregion

    public void exit2Menu()
    {
        Debug.Log("Exit to Menu");
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public enum UIPlatformType
{
    PC,
    Mobile
}
public class PauseMenu : MonoBehaviour
{
    public UIPlatformType PlatformType;

    public static bool GameisPaused = false;

    public GameObject pauseMenuUI, optionsMenuUI, dialogueBox, mobileUI, mobilePauseButton, healthBar, miniMap, inventory;

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
            case UIPlatformType.Mobile:
                mobileUI.SetActive(true);
                mobilePauseButton.SetActive(true);
                break;
        }
    }
    private void UI()
    {
        switch (PlatformType)
        {
            case UIPlatformType.PC:
                interfacePC();
                break;
            case UIPlatformType.Mobile:
                interfaceMobile();
                break;
        }
    }

    public void ResumeButton()
    {
        switch (PlatformType)
        {
            case UIPlatformType.PC:
                MainManager.Instance.IsPaused = false;

                pauseMenuUI.SetActive(false);
                miniMap.SetActive(true);
                healthBar.SetActive(true);

                Time.timeScale = 1f;

                
                GameisPaused = false;
                break;
            case UIPlatformType.Mobile:
                MainManager.Instance.IsPaused = false;

                pauseMenuUI.SetActive(false);
                miniMap.SetActive(true);
                healthBar.SetActive(true);
                mobileUI.SetActive(true);
                mobilePauseButton.SetActive(true);

                Time.timeScale = 1f;

                
                GameisPaused = false;
                break;
        }
        
    }

    public void PauseButton()
    {
        MainManager.Instance.IsPaused = true;

        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        miniMap.SetActive(false);
        healthBar.SetActive(false);
        mobileUI.SetActive(false);
        mobilePauseButton.SetActive(false);
        inventory.SetActive(false);

        Time.timeScale = 0f;

        GameisPaused = true;
        
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
        healthBar.SetActive(true);
        miniMap.SetActive(true);
        
        MainManager.Instance.IsPaused = false;

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        Time.timeScale = 1f;

        GameisPaused = false;
        
    }

    public void pcPause()
    {
        healthBar.SetActive(false);
        miniMap.SetActive(false);
        
        MainManager.Instance.IsPaused = true;

        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        inventory.SetActive(false);

        Time.timeScale = 0f;

        GameisPaused = true;
        
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
        
    }

    #endregion

    public void exit2Menu()
    {
        
    }

    
}

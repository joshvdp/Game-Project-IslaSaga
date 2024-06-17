using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using Player.Controls;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject pauseMenuUI, optionsMenuUI, dialogueBox, mobileUI, mobilePauseButton, healthBar, miniMap, inventory;

    public Button pauseButton;

    private void OnEnable()
    {
        GlobalEvents.Instance.FindEvent("On Change Platform Type PC").AddListener(checkPlatform);
        GlobalEvents.Instance.FindEvent("On Change Platform Type Mobile").AddListener(checkPlatform);
    }

    private void OnDisable()
    {
        GlobalEvents.Instance.FindEvent("On Change Platform Type PC").RemoveListener(checkPlatform);
        GlobalEvents.Instance.FindEvent("On Change Platform Type Mobile").RemoveListener(checkPlatform);
    }
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
        switch (MainManager.Instance.Settings.PlatformType)
        {
            case PlatformType.Mobile:
                mobileUI.SetActive(true);
                mobilePauseButton.SetActive(true);
                break;
            case PlatformType.PC:
                mobileUI.SetActive(false);
                mobilePauseButton.SetActive(false);
                break;
        }
    }
    private void UI()
    {
        switch (MainManager.Instance.Settings.PlatformType)
        {
            case PlatformType.PC:
                interfacePC();
                break;
            case PlatformType.Mobile:
                interfaceMobile();
                break;
        }
    }

    public void ResumeButton()
    {
        switch (MainManager.Instance.Settings.PlatformType)
        {
            case PlatformType.PC:
                pauseMenuUI.SetActive(false);
                miniMap.SetActive(true);
                healthBar.SetActive(true);
                break;
            
            case PlatformType.Mobile:
                pauseMenuUI.SetActive(false);
                miniMap.SetActive(true);
                healthBar.SetActive(true);
                mobileUI.SetActive(true);
                mobilePauseButton.SetActive(true);
                
                break;
        }
        
        SetTimePause(false);
    }

    public void PauseButton()
    {
        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        miniMap.SetActive(false);
        healthBar.SetActive(false);
        mobileUI.SetActive(false);
        mobilePauseButton.SetActive(false);
        inventory.SetActive(false);
        
        SetTimePause(true);
    }

    public void SetTimePause(bool flag)
    {
        Debug.Log("TEST");
        MainManager.Instance.IsPaused = flag;
        GameisPaused = flag;
        Time.timeScale = flag ? 0 : 1;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using Player.Controls;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;

    public GameObject pauseMenuUI, optionsMenuUI, dialogueBox, mobileUI, mobilePauseButton, healthBar, miniMap, inventory, QuestUI, KillCount;

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
                
                break;
        }
    }

    public void ResumeButton()
    {
        switch (MainManager.Instance.Settings.PlatformType)
        {
            case PlatformType.PC:
                MainManager.Instance.SetTimeScale(1);

                pauseMenuUI.SetActive(false);
                miniMap.SetActive(true);
                healthBar.SetActive(true);

                GameisPaused = false;
                break;
            case PlatformType.Mobile:
                MainManager.Instance.SetTimeScale(1);

                pauseMenuUI.SetActive(false);
                miniMap.SetActive(true);
                healthBar.SetActive(true);
                mobileUI.SetActive(true);
                mobilePauseButton.SetActive(true);
                QuestUI.SetActive(true);
                KillCount.SetActive(true);
                
                GameisPaused = false;
                break;
        }
        
    }

    public void PauseButton()
    {
        MainManager.Instance.SetTimeScale(0);

        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        miniMap.SetActive(false);
        healthBar.SetActive(false);
        mobileUI.SetActive(false);
        mobilePauseButton.SetActive(false);
        inventory.SetActive(false);
        QuestUI.SetActive(false);
        KillCount.SetActive(false);

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

        MainManager.Instance.SetTimeScale(1);

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        

        GameisPaused = false;
        
    }

    public void pcPause()
    {
        healthBar.SetActive(false);
        miniMap.SetActive(false);

        MainManager.Instance.SetTimeScale(0);

        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        inventory.SetActive(false);

        GameisPaused = true;
        
    }
    #endregion


    #region Mobile Pause Menu


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

        MainManager.Instance.SetTimeScale(1);

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        mobileUI.SetActive(true);
        mobilePauseButton.SetActive(true);
        


        GameisPaused = false;
        
    }

    public void mobilePause()
    {

        MainManager.Instance.SetTimeScale(0);

        pauseMenuUI.SetActive(true);
        dialogueBox.SetActive(false);
        mobileUI.SetActive(false);
        mobilePauseButton.SetActive(false);

        GameisPaused = true;
        
    }

    #endregion
    

    
}

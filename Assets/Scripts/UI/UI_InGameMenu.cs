using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_InGameMenu : MonoBehaviour
{
    [SerializeField] Button _mainMenu;

    void Start()
    {
        _mainMenu.onClick.AddListener(LoadMainMenu);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}

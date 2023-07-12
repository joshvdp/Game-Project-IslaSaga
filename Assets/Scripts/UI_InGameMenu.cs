using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameMenu : MonoBehaviour
{
    [SerializeField] Button _mainMenu;

    void Start()
    {
        _mainMenu.onClick.AddListener(LoadMainMenu);
    }

    void LoadMainMenu()
    {
        ScenesManager.Instance.LoadMainMenu();
    }
}

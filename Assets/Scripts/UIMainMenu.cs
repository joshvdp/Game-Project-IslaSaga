using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] Button _startGame;

    void Start()
    {
        _startGame.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        ScenesManager.Instance.LoadNewGame();
    }
    void QuitGame()
    {
        Debug.Log("Quit");
        //Application.Quit();

    }


}

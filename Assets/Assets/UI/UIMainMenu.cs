using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] Button _startGame;

    void Start()
    {
        _startGame.onClick.AddListener(StartNewGame);
    }

    void StartNewGame()
    {
        ScenesManager.Instance.LoadNewGame();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using StateMachine.Player;
using UnityEngine;

public class EditorTextHelper : MonoBehaviour
{
    [SerializeField] PlayerMonoStateMachine PlayerMachine;

    [SerializeField] TextMeshProUGUI StateNameText;
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        StateNameText.text = PlayerMachine.CurrentState.Data.name;
        StateNameText.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Vector3.up);
    }
}

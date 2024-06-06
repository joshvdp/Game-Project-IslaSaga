using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using NaughtyAttributes;
public class PopupUIManager : MonoBehaviour
{
    public static PopupUIManager Instance;

    [Header("Text Pop Up Elements"),SerializeField] GameObject TextPopupUIObject;
    [SerializeField] TextMeshProUGUI TextReference;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }


    public void SetActiveTextPopupUI(string textToPopup)
    {
        TextPopupUIObject.SetActive(true);
        TextReference.text = textToPopup;
    }
    public void SetInactiveTextPopupUI()
    {
        TextPopupUIObject.SetActive(false);
    }
}

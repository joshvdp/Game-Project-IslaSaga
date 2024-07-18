using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TextPopupTrigger : MonoBehaviour
{
    [SerializeField] string TextToPopup;
    [SerializeField] UnityEvent OnPopupTriggerEnter;
    [SerializeField] UnityEvent OnPopupTriggerExit;
    private void OnTriggerEnter(Collider other)
    {
        PopupUIManager.Instance?.SetActiveTextPopupUI(TextToPopup);
        OnPopupTriggerEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        PopupUIManager.Instance?.SetInactiveTextPopupUI();
        OnPopupTriggerExit?.Invoke();
    }

}

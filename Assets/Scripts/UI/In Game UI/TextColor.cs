using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextColor : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    

    TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        text.color = new Color32(95,106,78,255);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        text.color = new Color32(232, 204, 146,255);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_PC : MonoBehaviour
{
    

    public GameObject lastDia;



    RectTransform bg;

    private void Start()
    {
        bg = gameObject.GetComponent<RectTransform>();
    }

    

    void Update()
    {
        if (lastDia.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("MainMenu");
            }
            
        }
    }
}

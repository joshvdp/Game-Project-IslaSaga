using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExMarkCaller : MonoBehaviour
{
    public GameObject ExMark;

    public void Detect()
    {
        ExMark.SetActive(true);
    }
    public void Undetect()
    {
        ExMark.SetActive(false);
    }
}

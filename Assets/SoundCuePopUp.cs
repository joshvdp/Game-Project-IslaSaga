using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCuePopUp : MonoBehaviour
{
    public AudioSource SFX;
    public AudioClip cue;// Start is called before the first frame update
    void Start()
    {
        SFX.PlayOneShot(cue);
    }

    // Update is called once per frame
    
}

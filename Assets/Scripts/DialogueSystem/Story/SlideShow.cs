using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    public Image image;
    public Sprite[] allSprites;
    public float start, delay;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(start);

        foreach (var sprites in allSprites)
        {
            image.sprite = sprites;
            yield return new WaitForSeconds(delay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

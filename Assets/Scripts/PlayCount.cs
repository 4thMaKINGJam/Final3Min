using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public RawImage[] countImages;
    public AudioSource playBGM;
    private void Start()
    {
        // 이미지를 비활성화합니다.
        foreach (RawImage rawImage in countImages)
        {
            rawImage.enabled = false;
        }

        StartCoroutine(DisplayImages());
    }

    IEnumerator DisplayImages()
    {
        foreach (RawImage rawImage in countImages)
        {
            rawImage.enabled = true;    
            yield return new WaitForSeconds(1f);  

            rawImage.enabled = false;  
            yield return null; 
        }
        playBGM.Play();
    }
}

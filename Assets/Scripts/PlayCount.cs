using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public RawImage[] countImages;
    public AudioSource playBGM;
    public AudioSource originBGM;
    public AudioClip[] countSounds;

    private void Start()
    {
        foreach (RawImage rawImage in countImages)
        {
            rawImage.enabled = false;
        }

        StartCoroutine(DisplayImages());
    }

    IEnumerator DisplayImages()
    {
        for (int i = 0; i < countImages.Length; i++)
        {
            RawImage rawImage = countImages[i];
            rawImage.enabled = true;

            originBGM.PlayOneShot(countSounds[i]);

            yield return new WaitForSeconds(0.9f);

            rawImage.enabled = false;
        }

        playBGM.Play();
    }
}

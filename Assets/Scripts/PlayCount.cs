using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayCount : MonoBehaviour
{
    public RawImage[] countImages;
    public AudioSource playBGM;
    public AudioSource originBGM;
    public AudioClip[] countSounds;

    public GameObject blockObj;
    public OrderManager orderManager;

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

            yield return new WaitForSeconds(0.5f);

            rawImage.enabled = true;

            originBGM.PlayOneShot(countSounds[i]);

            yield return new WaitForSeconds(0.5f);

            rawImage.enabled = false;
        }
        GameManager.instance.stopwatch.Start();
        blockObj.SetActive(false);
        orderManager.CreateFirstOrder();
        playBGM.Play();
    }
}

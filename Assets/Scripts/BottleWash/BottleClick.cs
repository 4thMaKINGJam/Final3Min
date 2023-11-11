using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        gameObject.SetActive(false);
        WashMiniGame.bottleDirty++;
        BottleLeft.bottleLeft--;
    }
}

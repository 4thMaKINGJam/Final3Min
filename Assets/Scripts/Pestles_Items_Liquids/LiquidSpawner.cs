using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Liquid;

    void OnMouseDown(){
        Instantiate(Liquid,transform.position,Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
 [SerializeField]
 private GameObject Item;

 void OnMouseDown(){
    Instantiate(Item,transform.position,Quaternion.identity);
 }
}

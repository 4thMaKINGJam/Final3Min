using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaker : MonoBehaviour
{
    // Start is called before the first frame update
    public int food = 7;
    public int liquid = 0;
    public int itemCnt = 0;

    void Update(){
        if(food!=7){
            print(food);
            food=7;
        }
    }
}

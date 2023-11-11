using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaker : MonoBehaviour
{
    // Start is called before the first frame update
    public int food = 0;
    public int liquid = 0;
    public int itemCnt = 0;

    void Update(){
        if(food!=0){
            print(food);
            food=0;
        }
    }
}

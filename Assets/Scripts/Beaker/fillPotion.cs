using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillPotion : MonoBehaviour
{
    //베이스 재료 : 4 물 5 포도주 6 기름
    int sBaseLiquid = 0;
    //아이템 재료: 0 버섯 1 약초 2 씨앗 3 꽃
    int[] sItemArray = new int[4];
    //굽기  : 0 미완성 1 완성 2 탐
    int sCooked = 0;
    //아이템(고체) 개수
    int sItemCnt = 0;


    [SerializeField]
    private GameObject fire;
    private beaker beakerCtrl;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            initiate();
        }
    }

    void initiate()
    {
        beakerCtrl = this.GetComponent<beaker>();
        beakerCtrl.StopCouroutine();
        fire.SetActive(false);
        beakerCtrl.enabled = false;

        sender();
    }

    void sender()
    { //값을 전부 전달 받고
        sBaseLiquid = beakerCtrl.liquid;
        Array.Copy(beakerCtrl.item, sItemArray, 4);
        sCooked = beakerCtrl.cooked;
        sItemCnt = beakerCtrl.itemCnt;

        for (int i = 0; i < sItemArray.Length; i++) {
            Debug.Log(sItemArray[i]);
        }
        
        Debug.Log("-------");
        beakerCtrl.enabled = true;
    }
}

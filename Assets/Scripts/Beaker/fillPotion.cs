using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillPotion : MonoBehaviour
{
    [HideInInspector]//베이스 재료 : 4 물 5 포도주 6 기름
    public int sBaseLiquid = 0;
    
    [HideInInspector]//아이템 재료: 0 버섯 1 약초 2 씨앗 3 꽃
    public int[] sItemArray = new int[4];
    
    [HideInInspector]//굽기  : 0 미완성 1 완성 2 탐
    public int sCooked = 0;
    
    [HideInInspector]//아이템(고체) 개수
    public int sItemCnt = 0;
    bool flag = false;
    void Update()
    {
        if (flag)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = Mathf.Clamp(mousepos.x, -8.5f, 8.5f);
            float y = Mathf.Clamp(mousepos.y, -4.5f, 4.5f);
            transform.position = new Vector2(x, y);
        }
    }

    void OnMouseDown()
    {
        flag = true;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15, LayerMask.GetMask("Beaker"));
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Beaker")
            {
                beaker beakerCtrl = hit.collider.gameObject.GetComponent<beaker>();
                beakerCtrl.initiate();
                sendToPotion(beakerCtrl);
            }
        }
    }

    void sendToPotion(beaker beakerCtrl) {
        sBaseLiquid = beakerCtrl.liquid;
        Array.Copy(beakerCtrl.item, sItemArray, 4);
        sCooked = beakerCtrl.cooked;
        sItemCnt = beakerCtrl.itemCnt;

        Debug.Log("값 전달 받기! 굽기:" + sCooked);
        for (int i = 0; i < sItemArray.Length; i++)
        {
            Debug.Log(sItemArray[i]);
        }

        beakerCtrl.enabled = true;

    }
}

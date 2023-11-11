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

    public OrderManager comparePotion;
    [SerializeField]
    private Animator animator;

    void OnEnable() {
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = Mathf.Clamp(mousepos.x, -8.5f, 8.5f);
            float y = Mathf.Clamp(mousepos.y, -4.5f, 4.5f);
            transform.position = new Vector2(x, y);
        
    }

    void OnMouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15, LayerMask.GetMask("Beaker"));
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Beaker")
            {
                beaker beakerCtrl = hit.collider.gameObject.GetComponent<beaker>();
                beakerCtrl.initiate();
                sendToPotion(beakerCtrl);
            }
            else if (hit.collider.gameObject.tag == "Submit") {
                
                comparePotion.SubmitPotion(sBaseLiquid, sCooked, sItemArray);
                Debug.Log("베이스: " + sBaseLiquid);
                Debug.Log("굽기: " + sCooked);
                for (int i = 0; i < sItemArray.Length; i++) {
                    Debug.Log("재료 배열:" + i + "번째 " + sItemArray);
                }
                
                Destroy(this.gameObject);
            }
        }
    }

    void sendToPotion(beaker beakerCtrl) {
        int color_cook = 0;
        sBaseLiquid = beakerCtrl.liquid;
        Array.Copy(beakerCtrl.item, sItemArray, 4);
        sCooked = beakerCtrl.cooked;
        sItemCnt = beakerCtrl.itemCnt;
        //Debug.Log("베이스: "+ )

        if (sBaseLiquid == 4 && sCooked == 0)
        {
            color_cook = 40;
        }
        else if (sBaseLiquid == 4 && sCooked == 1) {
            color_cook = 41;
        }
        else if (sBaseLiquid == 5 && sCooked == 0)
        {
            color_cook = 50;
        }
        else if (sBaseLiquid == 5 && sCooked == 1)
        {
            color_cook = 51;
        }
        else if (sBaseLiquid == 6 && sCooked == 0)
        {
            color_cook = 60;
        }
        else if (sBaseLiquid == 6 && sCooked == 1)
        {
            color_cook = 61;
        }
        else if (sCooked == 2)
        {
            color_cook = 70;
        }
        animator.SetInteger("color_cook", color_cook);
        beakerCtrl.enabled = true;
    }
}

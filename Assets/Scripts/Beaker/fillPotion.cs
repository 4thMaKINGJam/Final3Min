using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillPotion : MonoBehaviour
{
    [HideInInspector]//���̽� ��� : 4 �� 5 ������ 6 �⸧
    public int sBaseLiquid = 0;
    
    [HideInInspector]//������ ���: 0 ���� 1 ���� 2 ���� 3 ��
    public int[] sItemArray = new int[4];
    
    [HideInInspector]//����  : 0 �̿ϼ� 1 �ϼ� 2 Ž
    public int sCooked = 0;
    
    [HideInInspector]//������(��ü) ����
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
                Debug.Log("���̽�: " + sBaseLiquid);
                Debug.Log("����: " + sCooked);
                for (int i = 0; i < sItemArray.Length; i++) {
                    Debug.Log("��� �迭:" + i + "��° " + sItemArray);
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
        //Debug.Log("���̽�: "+ )

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

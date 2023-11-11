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

        Debug.Log("�� ���� �ޱ�! ����:" + sCooked);
        for (int i = 0; i < sItemArray.Length; i++)
        {
            Debug.Log(sItemArray[i]);
        }

        beakerCtrl.enabled = true;

    }
}

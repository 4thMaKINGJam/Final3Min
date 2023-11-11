using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillPotion : MonoBehaviour
{
    //���̽� ��� : 4 �� 5 ������ 6 �⸧
    int sBaseLiquid = 0;
    //������ ���: 0 ���� 1 ���� 2 ���� 3 ��
    int[] sItemArray = new int[4];
    //����  : 0 �̿ϼ� 1 �ϼ� 2 Ž
    int sCooked = 0;
    //������(��ü) ����
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
    { //���� ���� ���� �ް�
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

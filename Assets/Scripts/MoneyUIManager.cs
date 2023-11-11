using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUIManager : MonoBehaviour
{
    TextMeshProUGUI moneyCnt;

    // Start is called before the first frame update
    void Start()
    {
        moneyCnt = GetComponent<TextMeshProUGUI>();
        moneyCnt.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        moneyCnt.text = GameManager.instance.GetMoney().ToString();
    }
}

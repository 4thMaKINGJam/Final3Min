using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    Stopwatch stopwatch;
    //private Potion potion;
    [SerializeField] private Slider _slider;
    [SerializeField] private Sprite[] BaseImg;
    [SerializeField] private Sprite[] ItemImg;

    private float ORDER_TIME = 24f * 1000;
    public bool timeOver;
    public int price;

    private int ITEM_COUNT = 4;

    private Color GREEN = Color.green;
    private Color YELLOW = Color.yellow;
    private Color RED = Color.red;

    private void Awake()
    {
        PickBase();

    }


    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        timeOver = false;
        SetValue(0);
        //92D050
        _slider.transform.GetChild(0).GetComponent<Image>().color = GREEN;
    }

    void PickBase()
    {

    }

    void PickItems()
    {
        System.Random prng = new System.Random();
        
        List<int> potion_item = new List<int>();

        // number of item
        int itemCount = prng.Next(1, 3);

        // assign item
        for (int i = 0; i < itemCount; i++)
        {
            potion_item.Add(1);
        }
        for (int i = 0; i < ITEM_COUNT - itemCount; i++)
        {
            potion_item.Add(0);
        }

        // shuffle
        for (int i=0; i < ITEM_COUNT; i++)
        {
            int random_idx = prng.Next(i, ITEM_COUNT - 1);

            int temp = potion_item[random_idx];
            potion_item[random_idx] = potion_item[i];
            potion_item[i] = temp;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!timeOver)
        {
            CheckTimer();
        }
        SetValue(stopwatch.ElapsedMilliseconds / ORDER_TIME);
    }

    void CheckTimer()
    {
        if (stopwatch.ElapsedMilliseconds <= ORDER_TIME / 3)
        {
            _slider.transform.GetChild(0).GetComponent<Image>().color = GREEN;
        }
        else if (stopwatch.ElapsedMilliseconds <= ORDER_TIME / 3 * 2)
        {
            _slider.transform.GetChild(0).GetComponent<Image>().color = YELLOW;
        }
        else if (stopwatch.ElapsedMilliseconds <= ORDER_TIME)
        {
            // change bar color
            _slider.transform.GetChild(0).GetComponent<Image>().color = RED;
        }
        else
        {
            UnityEngine.Debug.Log("The end");
            StopTimer();
            timeOver = true;
        }
    }

    void StopTimer()
    {
        stopwatch.Stop();
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }
}

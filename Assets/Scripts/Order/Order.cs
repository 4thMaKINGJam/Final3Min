using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    Stopwatch stopwatch;
    private Potion potion;
    [SerializeField] private Slider _slider;
    [SerializeField] private Sprite[] CompleteImg;
    [SerializeField] private Sprite[] ItemImg;

    private float ORDER_TIME = 24f * 1000;
    public bool timeOver;
    public int price;

    private int BASE_COUNT = 3;
    private int ITEM_COUNT = 4;

    private int ITEM_IDX = 0;
    private int COMPLETE_IDX = 2;

    private Color GREEN = Color.green;
    private Color YELLOW = Color.yellow;
    private Color RED = Color.red;

    private void Awake()
    {
        _slider.transform.GetChild(0).GetComponent<Image>().color = GREEN;

        for (int i = 0; i < 3; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
        }
        SetRecipe();
    }


    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        timeOver = false;
        SetSliderValue(0);
    }


    void SetRecipe()
    {
        System.Random prng = new System.Random();

        // -- set base
        int base_num = prng.Next(0, BASE_COUNT);
        transform.GetChild(COMPLETE_IDX).GetComponent<SpriteRenderer>().sprite = CompleteImg[base_num];

        // -- set items
        List<int> potionItem = new List<int>();
        // number of item
        int itemCount = prng.Next(1, 3);

        // assign item
        for (int i = 0; i < itemCount; i++)
        {
            potionItem.Add(1);
        }
        for (int i = 0; i < ITEM_COUNT - itemCount; i++)
        {
            potionItem.Add(0);
        }

        // shuffle
        for (int i=0; i < ITEM_COUNT; i++)
        {
            int random_idx = prng.Next(i, ITEM_COUNT - 1);

            int temp = potionItem[random_idx];
            potionItem[random_idx] = potionItem[i];
            potionItem[i] = temp;
        }

        int idx = ITEM_IDX;

        for (int i=0; i < ITEM_COUNT; i++)
        {
            if (potionItem[i] > 0)
            {
                transform.GetChild(idx++).GetComponent<SpriteRenderer>().sprite = ItemImg[i];
            }
        }

        this.potion = new Potion(base_num, potionItem);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!timeOver)
        {
            CheckTimer();
        }
        SetSliderValue(stopwatch.ElapsedMilliseconds / ORDER_TIME);
    }

    void CheckTimer()
    {
        if (stopwatch.ElapsedMilliseconds <= ORDER_TIME / 3)
        {
            setSliderColor(GREEN);
        }
        else if (stopwatch.ElapsedMilliseconds <= ORDER_TIME / 3 * 2)
        {
            setSliderColor(YELLOW);
        }
        else if (stopwatch.ElapsedMilliseconds <= ORDER_TIME)
        {
            setSliderColor(RED);
        }
        else
        {
            StopTimer();
            timeOver = true;
        }
    }

    private void StopTimer()
    {
        stopwatch.Stop();
    }

    private void SetSliderValue(float value)
    {
        _slider.value = value;
    }

    private void setSliderColor(Color c)
    {
        _slider.transform.GetChild(0).GetComponent<Image>().color = c;
    }
}

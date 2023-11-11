using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    Stopwatch stopwatch;

    private readonly float INITIAL_INTERVAL = 5f;
    private readonly int ORDER_COUNT_LIMIT = 3;
    private readonly float DEFAULT_DELAY = 3f;

    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound;
    [SerializeField] private AudioClip timeoutSound;
    [SerializeField] private AudioSource audioPlayer;

    private readonly List<GameObject> orderList = new List<GameObject>();
    private readonly List<Vector3> _position = new List<Vector3>();

    [SerializeField] private GameObject OrderPrefab;

    public WashMiniGame washer;
    public GameObject pricePrefab;

    private void Awake()
    {
        // collect position info
        _position.Add(transform.GetChild(1).gameObject.transform.position);
        _position.Add(transform.GetChild(2).gameObject.transform.position);
        _position.Add(transform.GetChild(3).gameObject.transform.position);

        for (int i = 0; i < ORDER_COUNT_LIMIT; i++)
        {
            transform.GetChild(i + 1).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        CheckTimeOver();
    }

    // create new order, randomly generate potion(reciepe)
    void CreateNewOrder()
    {
        if (orderList.Count >= ORDER_COUNT_LIMIT)
        {
            return;
        }
        GameObject new_order = Instantiate(OrderPrefab, _position[orderList.Count], transform.rotation);
        orderList.Add(new_order);
        transform.GetChild(orderList.Count).GetComponent<SpriteRenderer>().enabled = true;
    }

    void DiscardOrder(int idx)
    {
        // discard order
        GameObject temp = orderList[idx];
        orderList.RemoveAt(idx);

        // fix position
        for (int i=0; i < orderList.Count; i++)
        {
            transform.GetChild(i + 1).GetComponent<SpriteRenderer>().enabled = true;
            orderList[i].transform.position = _position[i];
        }

        for (int i=orderList.Count; i < ORDER_COUNT_LIMIT; i++)
        {
            transform.GetChild(i + 1).GetComponent<SpriteRenderer>().enabled = false;
        }

        Destroy(temp);
        Invoke(nameof(CreateNewOrder), DEFAULT_DELAY);
    }

    // submit new potion
    public void SubmitPotion(int baseNum, int cooked, int[] items)
    {
        washer.IncreaseDirtyBottle();

        UnityEngine.Debug.Log("submitted!");
        if (cooked != 1)
        {
            UnityEngine.Debug.Log("!!!@@@"+cooked);
            return;
        }

        for (int i = 0; i < orderList.Count; i++)
        {
            Order currentOrder = orderList[i].GetComponent<Order>();
            if (currentOrder.IsEqual(baseNum, items))
            {
                // add coin
                GameManager.instance.ChangeMoney(currentOrder.price);
                CreatePriceCanvas(currentOrder.price);

                DiscardOrder(i);
                
                UnityEngine.Debug.Log("correct");
                audioPlayer.PlayOneShot(correctSound);
                return;
            }
        }
        UnityEngine.Debug.Log("wrong");
        audioPlayer.PlayOneShot(wrongSound);
    }

    void CheckTimeOver()
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            if (orderList[i].GetComponent<Order>().timeOver)
            {
                audioPlayer.PlayOneShot(timeoutSound);
                DiscardOrder(i);
            }
        }
    }

    public void CreateFirstOrder()
    {
        CreateNewOrder();
        Invoke(nameof(CreateNewOrder), INITIAL_INTERVAL);
        Invoke(nameof(CreateNewOrder), INITIAL_INTERVAL * 2);
    }

    private void CreatePriceCanvas(int price)
    {
        GameObject priceCanvas = Instantiate(pricePrefab);
        Transform priceTextTransform = priceCanvas.transform.Find("PriceText");

        if (priceTextTransform != null)
        {
            TextMeshProUGUI priceText = priceTextTransform.GetComponent<TextMeshProUGUI>();

            // 찾은 priceText를 사용하여 텍스트 변경
            if (priceText != null)
            {
                priceText.SetText("+" + price); 
            }
            else
            {
                UnityEngine.Debug.LogWarning("priceText is not a TextMeshProUGUI component.");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("priceText not found under priceCanvas.");
        }
    }
}

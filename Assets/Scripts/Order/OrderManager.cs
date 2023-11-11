using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    Stopwatch stopwatch;

    private readonly float INITIAL_INTERVAL = 5f;
    private readonly int ORDER_COUNT_LIMIT = 3;
    private readonly List<GameObject> orderList = new List<GameObject>();
    private readonly List<Vector3> _position = new List<Vector3>();
    [SerializeField] private GameObject OrderPrefab;

    public WashMiniGame washer;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
        }

        // collect position info
        _position.Add(transform.GetChild(1).gameObject.transform.position);
        _position.Add(transform.GetChild(2).gameObject.transform.position);
        _position.Add(transform.GetChild(3).gameObject.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateNewOrder();
        Invoke(nameof(CreateNewOrder), INITIAL_INTERVAL);
        Invoke(nameof(CreateNewOrder), INITIAL_INTERVAL * 2);
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

    }

    void DiscardOrder(int idx)
    {
        // discard order
        GameObject temp = orderList[idx];
        orderList.RemoveAt(idx);

        // fix position
        for (int i=0; i < orderList.Count; i++)
        {
            orderList[i].transform.position = _position[i];
        }

        Destroy(temp);
        CreateNewOrder();
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
            if (orderList[i].GetComponent<Order>().IsEqual(baseNum, items))
            {
                // add coin
                DiscardOrder(i);
                UnityEngine.Debug.Log("correct");
                return;
            }
        }
        UnityEngine.Debug.Log("wrong");
    }

    void CheckTimeOver()
    {
        for (int i = 0; i < orderList.Count; i++)
        {
            if (orderList[i].GetComponent<Order>().timeOver)
            {
                DiscardOrder(i);
            }
        }
    }

}

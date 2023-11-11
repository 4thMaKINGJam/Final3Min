using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beaker : MonoBehaviour
{
    [HideInInspector]
    public int food = 0;
    /*포션에게 전달해야할 것*/
    public int liquid { get; private set; }
    public int[] item { get; private set; } = new int[4];
    public int cooked { get; private set; }

    public int itemCnt { get; private set; }
    int foodCnt;

    float waitTime;
    private Coroutine beakerTimer;

    [SerializeField]
    private Sprite[] potions = new Sprite[4];
    [SerializeField]
    private GameObject fire;
    private Animator animator;
    

    void OnEnable() {//enable 
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = potions[0];
        animator.SetInteger("CookRate", 0);

        food = 7;
        liquid = 0;
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = 0;
        }
        cooked = 0;
        itemCnt = 0;
        foodCnt = 0;
        waitTime = 7.0f;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            food = 2;
        }

        if (food !=7)
        {
            checkFood(food);
            food = 7;
        }
    }

    #region check food
    void checkFood(int foodType)
    {
        if (foodType < 4)//food가 재료면 item에 들어왔다고 추가
        {
            itemCnt++;
            item[foodType]++;
            for (int i = 0; i < item.Length; i++)
            {
                Debug.Log(item[i]);
            }
        }
        else if (foodType < 7)// food가 액체면 liquid에 추가, 비커 색깔 바꾸기
        {
            liquid = foodType;
            changeColor(liquid);
        }
        foodCnt++;
        timerCheck(foodCnt);
        return;
    }
    #endregion

    #region change color
    void changeColor(int baseColor) {
        animator.enabled = true;
        if (baseColor == 4){
            gameObject.GetComponent<SpriteRenderer>().sprite = potions[1];
        }
        else if (baseColor == 5) {
            gameObject.GetComponent<SpriteRenderer>().sprite = potions[2];
        } 
        else {
            gameObject.GetComponent<SpriteRenderer>().sprite = potions[3];
        }
    }
    #endregion

    #region beaker timer
    /***아이템이 들어올 때 7초, 3초 추가***/
    void timerCheck(int count)
    {
        if (count == 1)//처음이라면 7초 타이머 시작
        {
            beakerTimer = StartCoroutine(StartTimer());
            fire.SetActive(true);
        }
        else if (count > 1)//처음이 아니라면 3초를 더한다.
        {
            AddExtraTime();
        }
    }

    IEnumerator StartTimer()
    {
        while (waitTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            waitTime--;
        }
        animator.SetInteger("CookRate", 1);
        StartCoroutine(TimerFinished());
    }

    IEnumerator TimerFinished()
    {
        yield return new WaitForSeconds(3f);
        animator.SetInteger("CookRate", 2);
        fire.SetActive(false);
        this.GetComponent<beaker>().enabled = false;
    }

    void AddExtraTime()
    {
        waitTime += 3;
        //기존 코루틴 중지, 갱신된 시간으로 타이머 재시작
        StopCoroutine(beakerTimer);
        beakerTimer = StartCoroutine(StartTimer());
    }

    public void StopCouroutine() {        
        StopAllCoroutines();
    }
    #endregion
}

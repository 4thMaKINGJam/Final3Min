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
    private GameObject fire;
    private Animator animator;
    

    void OnEnable() {//enable 
        Debug.Log("B. 초기화 됐습니다!");
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
        
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
        animator.SetInteger("progress", cooked);

        
    }

        void Update()
    {
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
            Debug.Log("B. food 들어왔어요:"+ foodType);
            itemCnt++;
            item[foodType]++;
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
        animator.SetInteger("color", baseColor);
    }
    #endregion

    #region beaker timer
    /***아이템이 들어올 때 7초, 3초 추가***/
    void timerCheck(int count)
    {
        Debug.Log("시간 잴게요~" + count);
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
            Debug.Log("B.완성까지"+waitTime);
        }
        StartCoroutine(TimerFinished());
    }

    IEnumerator TimerFinished()
    {
        int timeExm = 3;
        cooked = 1;
        Debug.Log("B. 완성됐습니다." + cooked);
        animator.SetInteger("progress", cooked);//1
        while (timeExm>0) {
            yield return new WaitForSeconds(1f);
            timeExm--;
            Debug.Log("지금입니다. 성공의 시간!" + cooked);
        }
        
        fire.SetActive(false);
        cooked = 2;
        animator.SetInteger("progress", cooked);
        Debug.Log("B. 끝났수"+cooked);
        this.GetComponent<beaker>().enabled = false;
    }

    void AddExtraTime()
    {
        waitTime += 3;
        //기존 코루틴 중지, 갱신된 시간으로 타이머 재시작
        StopCoroutine(beakerTimer);
        Debug.Log("B. 3초 추가!");
        beakerTimer = StartCoroutine(StartTimer());
    }

    public void StopCouroutine() {        
        StopAllCoroutines();
    }
    #endregion

    public void initiate()//비커 설정 초기화
    {
        Debug.Log("비커 초기화");
        StopCouroutine();
        fire.SetActive(false);
        this.enabled = false;
    }
}

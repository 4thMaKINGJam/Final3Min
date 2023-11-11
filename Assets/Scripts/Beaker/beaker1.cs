    /*[HideInInspector]
    public int food = 0;

    public int liquid { get; private set; }
    public int[] item { get; private set; } = new int[4];
    public int cooked { get; private set; }

    float waitTime;
    private Coroutine beakerTimer;

    [SerializeField]
    private GameObject fire;
    private Animator animator;
    

    void OnEnable() {//enable 
        food = 7;
        liquid = 0;
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = 0;
        }
        cooked = 0;
        waitTime = 7.0f;
        
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
        animator.SetInteger("cooked", cooked);
    }

    void Update()
    {
        if (food != 7)
        {
            checkFood(food);
            food = 7;
            foodCnt++;
            timerCheck(foodCnt);
        }
    }

    #region change color
    void changeColor(int baseColor) {
        animator.enabled = true;
        animator.SetInteger("color", baseColor);
    }
    #endregion

    #region beaker timer
    /***아이템이 들어올 때 7초, 3초 추가***/
    /*void timerCheck(int count)
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
        cooked = 1;
        animator.SetInteger("cooked", cooked);//1
        yield return new WaitForSeconds(3f);
        
        fire.SetActive(false);
        cooked = 2;
        animator.SetInteger("cooked", cooked);
        Debug.Log("B. 끝났수"+cooked);
        this.GetComponent<beaker>().enabled = false;
    }

    void AddExtraTime()
    {
        waitTime += 3;
        StopCoroutine(beakerTimer);
        beakerTimer = StartCoroutine(StartTimer());
    }

    public void StopCouroutine() {        
        StopAllCoroutines();
    }
    #endregion

    public void initiate()
    {
        StopCouroutine();
        fire.SetActive(false);
        this.enabled = false;
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beaker : MonoBehaviour
{
    [HideInInspector]
    public int food = 0;
    /*���ǿ��� �����ؾ��� ��*/
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
        Debug.Log("B. �ʱ�ȭ �ƽ��ϴ�!");
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
        if (foodType < 4)//food�� ���� item�� ���Դٰ� �߰�
        {
            Debug.Log("B. food ���Ծ��:"+ foodType);
            itemCnt++;
            item[foodType]++;
        }
        else if (foodType < 7)// food�� ��ü�� liquid�� �߰�, ��Ŀ ���� �ٲٱ�
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
    /***�������� ���� �� 7��, 3�� �߰�***/
    void timerCheck(int count)
    {
        Debug.Log("�ð� ��Կ�~" + count);
        if (count == 1)//ó���̶�� 7�� Ÿ�̸� ����
        {
            beakerTimer = StartCoroutine(StartTimer());
            fire.SetActive(true);
        }
        else if (count > 1)//ó���� �ƴ϶�� 3�ʸ� ���Ѵ�.
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
            Debug.Log("B.�ϼ�����"+waitTime);
        }
        StartCoroutine(TimerFinished());
    }

    IEnumerator TimerFinished()
    {
        int timeExm = 3;
        cooked = 1;
        Debug.Log("B. �ϼ��ƽ��ϴ�." + cooked);
        animator.SetInteger("progress", cooked);//1
        while (timeExm>0) {
            yield return new WaitForSeconds(1f);
            timeExm--;
            Debug.Log("�����Դϴ�. ������ �ð�!" + cooked);
        }
        
        fire.SetActive(false);
        cooked = 2;
        animator.SetInteger("progress", cooked);
        Debug.Log("B. ������"+cooked);
        this.GetComponent<beaker>().enabled = false;
    }

    void AddExtraTime()
    {
        waitTime += 3;
        //���� �ڷ�ƾ ����, ���ŵ� �ð����� Ÿ�̸� �����
        StopCoroutine(beakerTimer);
        Debug.Log("B. 3�� �߰�!");
        beakerTimer = StartCoroutine(StartTimer());
    }

    public void StopCouroutine() {        
        StopAllCoroutines();
    }
    #endregion

    public void initiate()//��Ŀ ���� �ʱ�ȭ
    {
        Debug.Log("��Ŀ �ʱ�ȭ");
        StopCouroutine();
        fire.SetActive(false);
        this.enabled = false;
    }
}

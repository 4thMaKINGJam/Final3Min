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
    private Sprite[] potions = new Sprite[4];
    [SerializeField]
    private GameObject fire;
    private Animator animator;
    

    void OnEnable() {//enable 
        Debug.Log("B. �ʱ�ȭ �ƽ��ϴ�!");
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
        if(Input.GetMouseButtonDown(1)){
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
        animator.SetInteger("CookRate", 1);
        StartCoroutine(TimerFinished());
    }

    IEnumerator TimerFinished()
    {
        yield return new WaitForSeconds(3f);
        animator.SetInteger("CookRate", 2);
        fire.SetActive(false);
        Debug.Log("B. ������");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beaker : MonoBehaviour
{

    [HideInInspector]
    public int food = 0;
    public int liquid { get; private set; } = 0;
    public int[] item { get; private set; } = new int[4];

    public int itemCnt { get; private set; } = 0;
    int foodCnt = 0;

    float waitTime = 7.0f;
    private Coroutine beakerTimer;

    [SerializeField]
    private Sprite[] potions = new Sprite[4];
    [SerializeField]
    private GameObject fire;
    private Animator animator;
    

    void Start() {
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = potions[0];

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            food = 5;
        }
        if (Input.GetMouseButtonDown(1))
        {
            food = 4;
        }
        if (food > 0)
        {
            checkFood(food);
            food = 0;
        }
    }

    #region check food
    void checkFood(int foodType)
    {
        if (foodType < 3)//food�� ���� item�� ���Դٰ� �߰�
        {
            itemCnt++;
            item[foodType]++;
        }
        else if (foodType < 6)// food�� ��ü�� liquid�� �߰�, ��Ŀ ���� �ٲٱ�
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
        //fire.GetComponent<Animator>().enable=true;
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
        }
        animator.SetInteger("CookRate", 1);
        yield return new WaitForSeconds(3f);
        animator.SetInteger("CookRate", 2);
        fire.SetActive(false);
        TimerFinished();
    }

    void TimerFinished()
    {
        this.enabled = false;
    }

    void AddExtraTime()
    {
        waitTime += 3;
        //���� �ڷ�ƾ ����, ���ŵ� �ð����� Ÿ�̸� �����
        StopCoroutine(beakerTimer);
        animator.SetInteger("CookRate", 1);
        beakerTimer = StartCoroutine(StartTimer());
    }
    #endregion
}

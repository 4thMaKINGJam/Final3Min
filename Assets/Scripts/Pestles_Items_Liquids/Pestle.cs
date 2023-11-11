using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    public bool Isfull = false;
    public bool IsCooked = false;
    public bool GetIn = false;
    public int Item; // 0,1,2,3

    [SerializeField]
    private GameObject[] CookedItem;

    [SerializeField]
    private Sprite[] BeforeCook;

    [SerializeField]
    private Sprite[] AfterCook;
    private Sprite DefaultSprite;

    private bool MinigameStage = false;
    GameObject MinigameBar;  // Pestle의 하위 오브젝트
    GameObject MinigameRect;  // MinigameBar의 하위 오브젝트
    GameObject MinigameCircle;  // MinigameRect의 하위 오브젝트

    void Start() {
        DefaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        MinigameBar = transform.Find("MinigameBar").gameObject;
        MinigameRect = MinigameBar.transform.Find("MinigameRect").gameObject;
        MinigameCircle = MinigameRect.transform.Find("MinigameCircle").gameObject; 
    }

    void Update()
    {
        if(GetIn){
            Isfull = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = BeforeCook[Item];
            GetIn = false;
        }
        
    }

    void OnMouseDown(){
        if(MinigameStage){      
            double posX = MinigameCircle.transform.localPosition.x;
            if(posX < 0.5 && posX > -0.5)
            {
                IsCooked=true;
                gameObject.GetComponent<SpriteRenderer>().sprite = AfterCook[Item];
            }
            MinigameBar.SetActive(false);
            MinigameRect.SetActive(false);
            MinigameCircle.SetActive(false);
            MinigameStage = false; 
        }
        else{     
            if(Isfull){
                if(!IsCooked){  // 빻지 않은 상태라면
                    MinigameStage = true; 

                    float end = (float)0.5*(1-MinigameRect.transform.localScale.x);
                    MinigameRect.transform.localPosition = new Vector3(Random.Range(0f,end),0,0);  // 랜덤 영역 생성
                    
                    Transform t = MinigameBar.transform;
                    MinigameCircle.transform.position = new Vector3(t.position.x-t.lossyScale.x/2,t.position.y,t.position.z);

                    MinigameBar.SetActive(true);
                    MinigameRect.SetActive(true);
                    MinigameCircle.SetActive(true);
                }
                else{  // 빻은 상태라면
                    gameObject.GetComponent<SpriteRenderer>().sprite = DefaultSprite;
                    Instantiate(CookedItem[Item], transform.position, Quaternion.identity);
                    Isfull = false;
                    IsCooked = false;
                }
            }
            
        }
    }
}

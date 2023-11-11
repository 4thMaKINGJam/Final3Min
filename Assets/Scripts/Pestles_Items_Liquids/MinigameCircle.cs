using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MinigameCircle 오브젝트 만들어서 부착, 오브젝트 관계: Pestle > MinigameBar > MinigameRect > MinigameCircle

public class MinigameCircle : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;

    private float BarPosX;
    private float BarScaleX;
    void Start(){
        BarPosX = transform.parent.parent.position.x;
        BarScaleX = transform.parent.parent.lossyScale.x;
    }
    void Update()
    {
        if(transform.position.x + transform.lossyScale.x/2 > BarPosX + BarScaleX/2)
            transform.position = new Vector3(BarPosX - BarScaleX/2 + transform.lossyScale.x/2, transform.position.y, transform.position.z);
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}

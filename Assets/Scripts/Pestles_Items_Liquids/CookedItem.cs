using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedItem : MonoBehaviour
{
    [SerializeField]
    private int ItemType;   // 0,1,2,3

    // Update is called once per frame
    void Update()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Clamp(mousepos.x,-8.5f,8.5f);
        float y = Mathf.Clamp(mousepos.y,-4.5f,4.5f);
        transform.position = new Vector2(x,y);
    }

    void OnMouseDown(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.forward, 15, LayerMask.GetMask("Beaker","Bin"));
        if(hit){
            if(hit.collider.gameObject.tag == "Bin"){
                Destroy(gameObject);
            }
            else{
                Beaker beaker = hit.collider.gameObject.GetComponent<Beaker>();
                //if(beaker.liquid != 0){
                    if(beaker.itemCnt < 2){
                        beaker.food = ItemType;
                        Destroy(gameObject);
                    }
                //}
            }
        } 
    }
}
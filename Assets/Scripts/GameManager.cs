using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int money;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("BottleWash");
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}

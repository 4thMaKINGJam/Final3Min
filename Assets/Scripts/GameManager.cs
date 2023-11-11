using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Stopwatch stopwatch;

    public int money = 0;
    
    public bool MouseHasObject = false;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Play");
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene("Title");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

    public void ChangeMoney(int x)
    {
        this.money += x;
    }

    public int GetMoney()
    {
        return this.money;
    }
}

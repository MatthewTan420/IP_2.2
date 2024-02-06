using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TextMeshPro timerLb1;
    public float timer;
    private float time;
    public bool isEnd = true;
    public bool isFish;
    public bool isTrash;
    public AuthManager authManager;
    public GameObject obj;

    private void Start()
    {
        time = timer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isStarted();
        }

        if (isEnd == false)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                DisplayTime(timer);
            }
            else
            {
                timerLb1.text = "0:0";
                if (isFish == true)
                {
                    Debug.Log("fish");
                    //authManager.UpdatePrawn();
                }
                else if (isTrash == true)
                {
                    Debug.Log("trash");
                    //authManager.UpdateTrash();
                }
                isEnd = true;
            }
        }
        else
        {
            DisplayTime(timer);
            obj.SetActive(true);
        }
    }

    /// <summary>
    /// Display time
    /// </summary>
    private void DisplayTime(float displayTime)
    {
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        timerLb1.text = $"{minutes}:{seconds}";
    }

    public void isStarted()
    {
        timer = time;
        isEnd = false;
    }
}

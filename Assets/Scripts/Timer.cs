/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: AudioPlay
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// TextMeshPro for displaying the timer.
    /// </summary>
    public TextMeshPro timerLb1;

    /// <summary>
    /// Initial timer value.
    /// </summary>
    public float timer;

    /// <summary>
    /// Original timer value for resetting.
    /// </summary>
    private float time;

    /// <summary>
    /// Flag indicating whether the timer has reached zero.
    /// </summary>
    public bool isEnd = true;

    /// <summary>
    /// Flag indicating a specific event related to fish.
    /// </summary>
    public bool isFish;

    /// <summary>
    /// Flag indicating a specific event related to trash.
    /// </summary>
    public bool isTrash;

    /// <summary>
    /// AuthManager for handling updates based on timer completion.
    /// </summary>
    public AuthManager authManager;

    /// <summary>
    /// GameObject for managing visibility when the timer is completed.
    /// </summary>
    public GameObject obj;

    /// <summary>
    /// GameObject for managing visibility during the timer countdown.
    /// </summary>
    public GameObject obj1;

    /// <summary>
    /// GameObject representing a barrier.
    /// </summary>
    public GameObject barrier;

    /// <summary>
    /// Called on script initialization. Assigns the original timer value.
    /// </summary>
    private void Start()
    {
        time = timer;
    }

    /// <summary>
    /// Called once per frame. Manages the countdown timer, displays time, and handles events based on timer completion.
    /// </summary>
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
                    authManager.UpdatePrawn();
                }
                else if (isTrash == true)
                {
                    Debug.Log("trash");
                    authManager.UpdateTrash();
                }
                barrier.SetActive(false);
                isEnd = true;
            }
        }
        else
        {
            DisplayTime(timer);
            obj.SetActive(true);
            obj1.SetActive(false);
        }
    }

    /// <summary>
    /// Display time in minutes and seconds format.
    /// </summary>
    private void DisplayTime(float displayTime)
    {
        float minutes = Mathf.FloorToInt(displayTime / 60);
        float seconds = Mathf.FloorToInt(displayTime % 60);
        timerLb1.text = $"{minutes}:{seconds}";
    }

    /// <summary>
    /// Resets the timer and sets the isEnd flag to false, starting the countdown.
    /// </summary>
    public void isStarted()
    {
        timer = time;
        isEnd = false;
    }
}

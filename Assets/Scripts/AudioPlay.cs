/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: AudioPlay
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioSource audioS;
    public GameObject text;
    public float dur;

    /// <summary>
    /// Called when another Collider enters this GameObject's trigger collider.
    /// Checks if the entering object has the "Player" tag and calls infoPlay() if true.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            infoPlay();
        }
    }

    /// <summary>
    /// Plays the assigned audio, activates the information text, and schedules text deactivation after the specified duration.
    /// </summary>
    public void infoPlay()
    {
        audioS.Play();
        text.SetActive(true);
        Invoke(nameof(hideText), dur);
    }

    /// <summary>
    /// Deactivates the information text.
    /// </summary>
    private void hideText()
    {
        text.SetActive(false);
    }
}

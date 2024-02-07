/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Fishing
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    private GameObject fish;

    /// <summary>
    /// Called when another Collider enters this GameObject's trigger collider.
    /// Checks if the entering object has the "Player" tag and calls infoPlay() if true.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fish")
        {
            other.transform.SetParent(gameObject.transform);
            float randomNumber = Random.Range(5, 15);
            fish = other.gameObject;
            Invoke("fishEscape", randomNumber);
        }
    }

    /// <summary>
    /// Destroys the caught fish, simulating its escape from the player's grasp.
    /// </summary>
    public void fishEscape()
    {
        Destroy(fish);
    }
}

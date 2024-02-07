/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Distance
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    /// <summary>
    /// TextMeshPro for displaying the distance.
    /// </summary>
    public TextMeshPro textMeshPro;

    /// <summary>
    /// GameObject representing the target for distance calculation.
    /// </summary>
    public GameObject cube;

    /// <summary>
    /// Distance value between the attached GameObject and the cube.
    /// </summary>
    public float num;

    /// <summary>
    /// Flag indicating whether the player is far from the cube.
    /// </summary>
    public bool isFar = true;

    /// <summary>
    /// Reference to the AuthManager for handling distance updates.
    /// </summary>
    public AuthManager authManager;

    /// <summary>
    /// Called once per frame. Calculates and displays the distance between the attached GameObject and the cube.
    /// Triggers an update through AuthManager if the player is far from the cube.
    /// </summary>
    void Update()
    {
        num = (cube.transform.position - transform.position).magnitude;

        if (num <= 5.0f)
        {
            textMeshPro.text = num.ToString();
        }
        else
        {
            isFar = true;
        }
    }

    /// <summary>
    /// Called when another Collider enters this GameObject's trigger collider.
    /// Checks if the entering object has the "Player" tag and is far from the cube.
    /// If true, updates the cube reference, sets isFar to false, and triggers an update through AuthManager.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isFar == true)
        {
            cube = other.gameObject;
            isFar = false;
            authManager.UpdateDistance();
        }
    }
}

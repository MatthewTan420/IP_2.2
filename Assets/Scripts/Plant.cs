/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Plant
 */

using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    /// <summary>
    /// GameObject representing the seed stage of the plant.
    /// </summary>
    public GameObject seed;

    /// <summary>
    /// GameObject representing the sapling stage of the plant.
    /// </summary>
    public GameObject sapling;

    /// <summary>
    /// GameObject representing the mature plant stage.
    /// </summary>
    public GameObject plant;

    /// <summary>
    /// GameObject representing the final object after full growth.
    /// </summary>
    public GameObject obj;

    /// <summary>
    /// GameObject for displaying a message when there's no sunlight.
    /// </summary>
    public GameObject noSunTxt;

    /// <summary>
    /// Animator for handling growth animations.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Flag indicating whether the plant has been watered.
    /// </summary>
    public bool watered = false;

    /// <summary>
    /// Flag indicating whether the plant is receiving sunlight.
    /// </summary>
    public bool isSun;

    /// <summary>
    /// Called when another Collider enters this GameObject's trigger collider.
    /// Manages plant growth based on interactions with water, sunlight, and fertile soil.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water" && gameObject.tag == "Plant")
        {
            animator.SetBool("isWater", true);
            watered = true;
        }

        if (other.gameObject.tag == "Fertile" && gameObject.tag == "Plant")
        {
            if (watered == true && isSun == true)
            {
                animator.SetBool("isFertil", true);
                Invoke(nameof(objActive), 10.0f);
            }
            else if (watered == true && isSun == false)
            {
                noSunTxt.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Activates the final object after a specified delay.
    /// </summary>
    private void objActive()
    {
        obj.SetActive(true);
    }
}

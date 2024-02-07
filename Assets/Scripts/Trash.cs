/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Trash
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    /// <summary>
    /// TextMeshPro for displaying numeric values.
    /// </summary>
    public TextMeshPro textMeshPro;

    /// <summary>
    /// Number of collected items.
    /// </summary>
    public int num = 0;

    /// <summary>
    /// Number of fish collected in a bucket.
    /// </summary>
    public int pnum = 0;

    /// <summary>
    /// Subnumber used for counting a subset of items before updating the main count.
    /// </summary>
    public int subnum = 0;

    /// <summary>
    /// GameObject representing a general text display.
    /// </summary>
    public GameObject text;

    /// <summary>
    /// GameObject representing a plant.
    /// </summary>
    public GameObject plant;

    /// <summary>
    /// GameObject representing soil.
    /// </summary>
    public GameObject soil;

    /// <summary>
    /// GameObject representing a seed.
    /// </summary>
    public GameObject seed;

    /// <summary>
    /// Flag indicating whether digging is allowed.
    /// </summary>
    private bool isDig = false;

    /// <summary>
    /// Resets the numeric values to zero and updates the TextMeshPro text accordingly.
    /// </summary>
    public void reset()
    {
        num = 0;
        pnum = 0;
        subnum = 0;
        textMeshPro.text = "0";
    }

    /// <summary>
    /// Called when another Collider enters this GameObject's trigger collider.
    /// Handles various interactions based on the entered object's tag.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trash" && gameObject.tag == "Dustbin")
        {
            num += 1;
            textMeshPro.text = num.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Education" && text != null)
        {
            text.SetActive(true);
        }

        if (other.gameObject.tag == "Fish" && gameObject.tag == "Bucket")
        {
            pnum += 1;
            textMeshPro.text = pnum.ToString();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Trash" && gameObject.tag == "Bucket")
        {
            subnum += 1;
            if (subnum == 5)
            {
                pnum += 1;
                textMeshPro.text = pnum.ToString();
                subnum = 0;
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Shovel" && gameObject.tag == "Spot")
        {
            soil.SetActive(true);
            seed.SetActive(true);
            isDig = true;
        }

        if (other.gameObject.tag == "Seed" && gameObject.tag == "Spot")
        {
            if (isDig == true)
            {
                plant.SetActive(true);
                Destroy(other.gameObject);
                Destroy(gameObject);
            }

        }
    }
}

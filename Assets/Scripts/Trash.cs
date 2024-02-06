using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public int num = 0;
    public int pnum = 0;
    public int subnum = 0;

    public GameObject text;
    public GameObject plant;
    public GameObject soil;
    public GameObject seed;

    private bool isDig = false;

    public void reset()
    {
        num = 0;
        pnum = 0;
        subnum = 0;
        textMeshPro.text = "0";
}

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public int num = 0;
    public int subnum = 0;

    public GameObject text;

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
            num += 1;
            textMeshPro.text = num.ToString();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Trash" && gameObject.tag == "Bucket")
        {
            subnum += 1;
            if (subnum == 5)
            {
                num += 1;
                textMeshPro.text = num.ToString();
                subnum = 0;
            }
            Destroy(other.gameObject);
        }
    }
}

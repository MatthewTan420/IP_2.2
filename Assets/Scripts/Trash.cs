using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public int num = 0;

    public GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trash" && gameObject.tag == "Dustbin")
        {
            num += 1;
            textMeshPro.text = num.ToString();
            Destroy(other);
        }

        if (other.gameObject.tag == "Education" && text != null)
        {
            text.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    private int num = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trash" && gameObject.tag == "Dustbin")
        {
            num += 1;
            textMeshPro.text = num.ToString();
            Destroy(other);
        }
    }
}

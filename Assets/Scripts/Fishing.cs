using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    private GameObject fish;

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

    public void fishEscape()
    {
        Destroy(fish);
    }
}

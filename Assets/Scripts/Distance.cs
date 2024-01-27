using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Distance : MonoBehaviour
{
    public GameObject bruh;
    public TextMeshPro textMeshPro;
    public GameObject cube;

    public float num;
    public bool isFar = true;
    public AuthManager authManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isFar == true)
        {
            bruh.SetActive(true);
            cube = other.gameObject;
            isFar = false;
            authManager.UpdateDistance();
        }
    }
}

using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject seed;
    public GameObject sapling;
    public GameObject plant;

    public Animator animator;
    public bool watered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water" && gameObject.tag == "Plant")
        {
            animator.SetBool("isWater", true);
            watered = true;
        }

        if (other.gameObject.tag == "Fertile" && gameObject.tag == "Plant")
        {
            if (watered == true)
            {
                animator.SetBool("isFertil", true);
            }
        }
    }
}

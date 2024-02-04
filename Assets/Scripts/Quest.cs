using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public Screenshot Screenshot;
    public GameObject barrier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Screenshot.isCroc == true && Screenshot.isTree == true)
        {
            barrier.SetActive(false);
        }
    }
}

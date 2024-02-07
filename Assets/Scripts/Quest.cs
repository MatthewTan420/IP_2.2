/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Quest
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public Screenshot Screenshot;
    public GameObject barrier;

    // Update is called once per frame
    void Update()
    {
        if (Screenshot.isCroc == true && Screenshot.isTree == true)
        {
            barrier.SetActive(false);
        }
    }
}

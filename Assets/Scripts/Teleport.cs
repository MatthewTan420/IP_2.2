/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: AudioPlay
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    /// <summary>
    /// Called when another Collider enters this GameObject's trigger collider.
    /// Checks the tag of the entering object and loads the corresponding scene.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "interior")
        {
            SceneManager.LoadScene(2);
        }

        if (other.gameObject.tag == "exterior")
        {
            SceneManager.LoadScene(1);
        }
    }
}

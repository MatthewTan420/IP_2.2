/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Pouring
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pouring : MonoBehaviour
{
    /// <summary>
    /// The ParticleSystem used to simulate pouring water.
    /// </summary>
    public ParticleSystem water;

    /// <summary>
    /// Update is called once per frame. Checks the object's orientation and plays/stops the water ParticleSystem accordingly.
    /// </summary>
    void Update()
    {
        // Check if the object's forward direction aligns within the specified angle range (60 degrees) from the downward vector.
        if (Vector3.Angle(Vector3.down, transform.forward) <= 60f)
        {
            water.Play();
        }
        else
        {
            water.Stop();
        }
    }
}

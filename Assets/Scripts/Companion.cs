/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Companion
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
    /// <summary>
    /// The NavMeshAgent for controlling the AI's movement.
    /// </summary>
    public NavMeshAgent ai;

    /// <summary>
    /// Transform representing the player.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Animator for controlling AI animations.
    /// </summary>
    public Animator aiAnim;

    /// <summary>
    /// GameObject representing a bird.
    /// </summary>
    public GameObject bird;

    /// <summary>
    /// Destination position for the AI.
    /// </summary>
    Vector3 dest;

    /// <summary>
    /// Flag indicating whether the AI has been fed with a seed.
    /// </summary>
    public bool isFed = false;

    /// <summary>
    /// GameObject used for display purposes.
    /// </summary>
    public GameObject text;

    /// <summary>
    /// AuthManager for updating a component after the AI is fed.
    /// </summary>
    public AuthManager authManager;

    /// <summary>
    /// Called once per frame. Manages AI movement, animations, and bird position based on feeding status.
    /// </summary>
    void Update()
    {
        if (isFed)
        {
            dest = player.position;
            ai.destination = dest;

            if (ai.remainingDistance <= ai.stoppingDistance)
            {
                aiAnim.SetBool("isFar", false);
                aiAnim.SetBool("isClose", true);
                bird.transform.position = new Vector3(bird.transform.position.x, 7, bird.transform.position.z);
            }
            else
            {
                aiAnim.SetBool("isClose", false);
                aiAnim.SetBool("isFar", true);
                bird.transform.position = new Vector3(bird.transform.position.x, 8, bird.transform.position.z);
            }
        }
        else
        {
            aiAnim.SetBool("isClose", true);
            bird.transform.position = new Vector3(bird.transform.position.x, 7, bird.transform.position.z);
        }
    }

    /// <summary>
    /// Called when another Collider enters this GameObject's trigger collider.
    /// Checks if the entering object has the "Seed" tag, sets isFed to true, destroys the seed, hides a text GameObject,
    /// and updates a component through AuthManager.
    /// </summary>
    /// <param name="other">The Collider of the entering object.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Seed")
        {
            isFed = true;
            Destroy(other.gameObject);
            text.SetActive(false);
            authManager.UpdateComp();
        }
    }
}

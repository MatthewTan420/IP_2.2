/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Crocodile
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crocodile : MonoBehaviour
{
    /// <summary>
    /// The NavMeshAgent component for controlling the enemy's movement.
    /// </summary>
    public NavMeshAgent agent;

    /// <summary>
    /// LayerMask to determine what is considered as ground for raycasting.
    /// </summary>
    public LayerMask whatIsGround;

    /// <summary>
    /// LayerMask to determine what is considered as the player.
    /// </summary>
    public LayerMask whatIsPlayer;

    /// <summary>
    /// The position of the walk point.
    /// </summary>
    public Vector3 walkPoint;

    /// <summary>
    /// Flag indicating if a walk point is set.
    /// </summary>
    public bool walkPointSet;

    /// <summary>
    /// Range within which the enemy searches for a walk point.
    /// </summary>
    public float walkPointRange;

    /// <summary>
    /// Animator component for controlling animations.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Called on script initialization. Assigns the NavMeshAgent component.
    /// </summary>
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Called once per frame. Handles enemy patrolling behavior.
    /// </summary>
    private void Update()
    {
        Patroling();
    }

    /// <summary>
    /// Manages the enemy's patrolling behavior.
    /// </summary>
    private void Patroling()
    {
        if (!walkPointSet)
        {
            Invoke(nameof(SearchWalkPoint), 10.0f);
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool("isMove", false);
        }
    }

    /// <summary>
    /// Searches for a random walk point within the specified range and sets it as the destination.
    /// </summary>
    private void SearchWalkPoint()
    {
        // Calculate a random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Raycast to ensure the walk point is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            animator.SetBool("isMove", true);
        }
    }
}

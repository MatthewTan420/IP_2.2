using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
    public NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;
    public GameObject bird;
    Vector3 dest;

    void Update()
    {
        dest = player.position;
        ai.destination = dest;
        if (ai.remainingDistance <= ai.stoppingDistance)
        {
            aiAnim.SetBool("isFar", false);
            aiAnim.SetBool("isClose", true);
            //Debug.Log(bird.transform.position.y);
            bird.transform.position = new Vector3(bird.transform.position.x, -1, bird.transform.position.z);
        }
        else
        {
            aiAnim.SetBool("isClose", false);
            aiAnim.SetBool("isFar", true);
            bird.transform.position = new Vector3(bird.transform.position.x, 0, bird.transform.position.z);
        }
    }
}

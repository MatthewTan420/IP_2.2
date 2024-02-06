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
    public bool isFed = false;

    public AuthManager authManager;

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Seed")
        {
            isFed = true;
            Destroy(other.gameObject);
            authManager.UpdateComp();
        }
    }
}

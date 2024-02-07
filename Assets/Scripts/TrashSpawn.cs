/*
 * Author: Matthew, Seth, Wee Kiat, Isabel, Clifford
 * Date: 8/2/2024
 * Description: Spawn
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    /// <summary>
    /// GameObject representing the trash object to be spawned.
    /// </summary>
    public GameObject trash;

    /// <summary>
    /// Rate at which trash objects are spawned (in seconds).
    /// </summary>
    public float firerate;

    /// <summary>
    /// Time of the next allowed trash spawn.
    /// </summary>
    public float nextfire;

    /// <summary>
    /// Range for the x-axis position of spawned trash.
    /// </summary>
    public float x1;

    /// <summary>
    /// Range for the x-axis position of spawned trash.
    /// </summary>
    public float x2;

    /// <summary>
    /// Range for the y-axis position of spawned trash.
    /// </summary>
    public float y1;

    /// <summary>
    /// Range for the y-axis position of spawned trash.
    /// </summary>
    public float y2;

    /// <summary>
    /// Range for the z-axis position of spawned trash.
    /// </summary>
    public float z1;

    /// <summary>
    /// Range for the z-axis position of spawned trash.
    /// </summary>
    public float z2;

    /// <summary>
    /// Called when the script starts. Spawns initial trash objects.
    /// </summary>
    void Start()
    {
        SpawnTrash();
        SpawnTrash();
        SpawnTrash();
        SpawnTrash();
    }

    /// <summary>
    /// Called once per frame. Checks if it's time to spawn a new trash object and spawns it.
    /// </summary>
    void Update()
    {
        if (Time.time > nextfire)
        {
            SpawnTrash();
            nextfire = Time.time + firerate;
        }
    }

    /// <summary>
    /// Spawns a trash object at a random position within the specified ranges.
    /// </summary>
    public void SpawnTrash()
    {
        float spawnPointX = Random.Range(x1, x2);
        float spawnPointY = Random.Range(y1, y2);
        float spawnPointZ = Random.Range(z1, z2);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        Instantiate(trash, spawnPosition, Quaternion.identity);
    }
}

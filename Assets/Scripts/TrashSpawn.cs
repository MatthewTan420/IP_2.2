using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    public GameObject trash;
    public float firerate;
    public float nextfire;

    void Start()
    {
        SpawnTrash();
        SpawnTrash();
        SpawnTrash();
        SpawnTrash();
        SpawnTrash();
    }

    void Update()
    {
        if (Time.time > nextfire)
        {
            SpawnTrash();
            nextfire = Time.time + firerate;
        }
    }

    public void SpawnTrash()
    {
        int spawnPointX = Random.Range(6, 9);
        int spawnPointY = Random.Range(0, 1);
        int spawnPointZ = Random.Range(-4, 4);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        Instantiate(trash, spawnPosition, Quaternion.identity);
    }
}

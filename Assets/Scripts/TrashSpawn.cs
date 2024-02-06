using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    public GameObject trash;
    public float firerate;
    public float nextfire;

    public float x1;
    public float x2;
    public float y1;
    public float y2;
    public float z1;
    public float z2;

    void Start()
    {
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
        float spawnPointX = Random.Range(x1, x2);
        float spawnPointY = Random.Range(y1, y2);
        float spawnPointZ = Random.Range(z1, z2);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        Instantiate(trash, spawnPosition, Quaternion.identity);
    }
}

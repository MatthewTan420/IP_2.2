using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    public GameObject trash;
    public float firerate;
    public float nextfire;

    public int x1;
    public int x2;
    public int y1;
    public int y2;
    public int z1;
    public int z2;

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
        int spawnPointX = Random.Range(x1, x2);
        int spawnPointY = Random.Range(y1, y2);
        int spawnPointZ = Random.Range(z1, z2);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        Instantiate(trash, spawnPosition, Quaternion.identity);
    }
}

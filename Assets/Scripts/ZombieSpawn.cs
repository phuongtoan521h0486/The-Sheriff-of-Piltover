using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [Header("ZombieSpawn Var")]
    public GameObject zombiePrefab;
    public Transform[] zombieSpawnPosition;
    private int spawnPointCurrent = 0;

    public static float amountZombies = 10f;

    private float repeatCycle = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            Destroy(gameObject, amountZombies);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            ObjectivesComplete.occurrence.GetObjectivesDone("task1");
        }
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefab, zombieSpawnPosition[spawnPointCurrent].position, zombieSpawnPosition[spawnPointCurrent].rotation);
        spawnPointCurrent++;
        if(spawnPointCurrent >= zombieSpawnPosition.Length)
        {
            spawnPointCurrent = 0;
        }
    }
}
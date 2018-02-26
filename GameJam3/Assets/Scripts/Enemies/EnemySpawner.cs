using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Determines what type of enemy should the spawner spawn.
    public GameObject enemy;
    public int spawnInterval;

    GameObject[] spawnpoint;
    GameObject cloneOfEnemy;
    GameObject[] enemies;
    int spawnTimer, timeLimit = 90, spawn, multiplier = 1;
    int zombieCap;

    int numberOfZobies = 7;
    int divisor = 2;
    // Use this for initialization
    void Awake()
    {
        spawnTimer = 0;
        spawnpoint = GameObject.FindGameObjectsWithTag("Spawnpoint");
        timeLimit /= (GameVariables.difficultyLevel * GameVariables.hardModeMultiplier);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Timer.timeRunOut) multiplier = 2;
        zombieCap = numberOfZobies * GameVariables.difficultyLevel * multiplier * GameVariables.hardModeMultiplier;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        spawnTimer++;

        if (spawnTimer % (60 * timeLimit) == 0 && spawnInterval > 1)
        {

            spawnInterval /= divisor;
        }
        if (spawnTimer % (60 * spawnInterval) == 0)
        {

            spawn = (int)Random.Range(0, spawnpoint.Length);
            if (!EnemiesKilled.zombieLimit && (zombieCap >= enemies.Length))
            {
                cloneOfEnemy = Instantiate(enemy, spawnpoint[spawn].transform.position, enemy.transform.rotation);
                if (EnemiesKilled.KillMode)
                {
                    EnemiesKilled.zombieCounter++;
                }
            }
        }
        if (cloneOfEnemy != null)
        {
            cloneOfEnemy.SetActive(true);
        }

    }
}

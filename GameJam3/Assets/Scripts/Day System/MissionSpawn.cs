using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSpawn : MonoBehaviour
{

    public int unsafeAreaSpawnChance;
    public static int amountOfMissions = 1;
    int spawnedMissions = 0;
    public static GameObject[] areas;
    AreaBehaviour[] areaBehaviour;

    // Use this for initialization
    void Awake()
    {
        //Unpause the game if it still happened to be paused
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }

        areas = GameObject.FindGameObjectsWithTag("Area"); //Create an array containing all areas
        areaBehaviour = new AreaBehaviour[areas.Length];

        //Get the areaBehaviour script that is attached to all areas
        for (int i = 0; i < areas.Length; i++)
        {
            areaBehaviour[i] = areas[i].GetComponent<AreaBehaviour>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Missions will keep spawning as long as the amount of spawned missions is smaller than the amount of missions that need to be spawned
        if (spawnedMissions < amountOfMissions)
        {
            SpawnRandomMissions();
        }
    }

    public void SpawnRandomMissions()
    {
        //Random number between 1 and 100 to determine where a mission spawns (safe or unsafe area)
        var diceRoll = Random.Range(1, 101);

        if (diceRoll <= unsafeAreaSpawnChance)
        {
            var randomUnsafeArea = Random.Range(0, areas.Length);

            if (!GameVariables.areasAvailable[randomUnsafeArea] && areaBehaviour[randomUnsafeArea].isUnsafe)
            {
                //spawn mission in a random "unsafe area" (the chance that this occurs equals the value of the "unsafeAreaSpawnChance" variable)
                GameVariables.areasAvailable[randomUnsafeArea] = true;
                spawnedMissions += 1;
            }
        }
        else if (diceRoll > unsafeAreaSpawnChance)
        {
            var randomSafeArea = Random.Range(0, areas.Length);

            if (!GameVariables.areasAvailable[randomSafeArea] && !areaBehaviour[randomSafeArea].isUnsafe)
            {
                //spawn mission in a random "safe area" (the chance that this occurs equals 100 minus the value of the "unsafeAreaSpawnChance" variable)
                GameVariables.areasAvailable[randomSafeArea] = true;
                spawnedMissions += 1;

            }
        }
    }
}

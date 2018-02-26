using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemiesKilled : MonoBehaviour
{

    // The number of zombies in kill count is determined by the difficulty of the zone and the hardmode multiplier
    int maxZombiesKilled = 30 * GameVariables.difficultyLevel * GameVariables.hardModeMultiplier;
    int zombiesKilled = 0;
    public static bool KillMode = false, zombieLimit = false;
    Text zombiesKilledText;
    // Counts the number of zombies which have been spawned in the map, so it doesn't spawn more zombies than the number of zombies that you have to kill.
    public static int zombieCounter = 0;
    bool wonMission;
    // Use this for initialization
    void Awake()
    {
        zombiesKilled = maxZombiesKilled;
        ResetCounter();
        zombiesKilledText = GetComponent<Text>();
        KillMode = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        zombiesKilledText.text = "" + zombiesKilled;
        if (zombiesKilled <= 0)
        {
            wonMission = true;
        }

        if (wonMission)
        {
            FindObjectOfType<BusRescue>().MissionStatus("win");
        }
        else
        {
            FindObjectOfType<BusRescue>().MissionStatus("lose");
        }

        if (zombieCounter >= maxZombiesKilled)
        {
            zombieLimit = true;
        }
    }
    public static void ResetCounter()
    {
        zombieCounter = 0;
        zombieLimit = false;
    }
    //The kill counter will drop if zombies are killed in killmode. It is called in enemyBehaviour
    public void EnemyKilled()
    {
        zombiesKilled--;
    }

}

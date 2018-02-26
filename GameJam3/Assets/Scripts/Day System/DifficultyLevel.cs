using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
    float chanceMoreMission;
    float spawnOneMission, spawnTwoMissions, spawnThreeMissions;
    AreaBehaviour[] areaBehaviour;

    // Use this for initialization
    private void Awake()
    {
        changeMissionSpawn(); //Determines how many missions will be spawned
    }

    private void Start()
    {
        areaBehaviour = new AreaBehaviour[MissionSpawn.areas.Length];

        for (int i = 0; i < MissionSpawn.areas.Length; i++)
        {
            areaBehaviour[i] = MissionSpawn.areas[i].GetComponent<AreaBehaviour>();
        }

        MissionDifficulty();
    }

    void MissionDifficulty()
    {
        for (int i = 0; i < GameVariables.difficultyLevels.Length; i++)
        {
            //When the difficulty level of a spawned mission is equal to 0, the type of that mission is set to Rescue and the difficulty is set to 1
            //If the mission is in an "unsafe" area, the difficulty is set to 2 + the amount of weeks that have passed
            if (GameVariables.difficultyLevels[i] == 0)
            {
                GameVariables.typeMission[i] = "Rescue";
                if (areaBehaviour[i].isUnsafe)
                {
                    GameVariables.difficultyLevels[i] = 2 + GameVariables.currentDay / 7;
                }
                else
                {
                    GameVariables.difficultyLevels[i] = 1;
                }
            }
        }
    }

    //This method is called every time the Overworld scene is loaded
    //The amount of missions that spawn is determined here
    void changeMissionSpawn()
    {
        chanceMoreMission = Random.Range(1, 101);
        spawnOneMission = Mathf.Max(GameVariables.currentSpawnOneMission, 25);
        spawnTwoMissions = Mathf.Min(GameVariables.currentSpawnTwoMissions, 50);
        spawnThreeMissions = Mathf.Min(GameVariables.currentSpawnThreeMissions, 25);

        if (chanceMoreMission <= spawnOneMission)
        {
            MissionSpawn.amountOfMissions = 1;
        }
        else if (chanceMoreMission <= (spawnOneMission + spawnTwoMissions))
        {
            MissionSpawn.amountOfMissions = 2;
        }
        else
        {
            MissionSpawn.amountOfMissions = 3;
        }

        //Every time 7 days have passed, the chance of multiple mission spawns will be increased
        if ((Mathf.Max(GameVariables.currentDay, 1)) % 7 == 0)
        {
            GameVariables.currentSpawnOneMission -= 10;
            GameVariables.currentSpawnTwoMissions += 9;
            GameVariables.currentSpawnThreeMissions += 1;
        }
    }
}

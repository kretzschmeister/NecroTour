using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MissionManagementScript
{
    public static int currentMissionIndex;

    /*This method takes 2 arguments: the first one being the result of the mission (can be either "win" or "lose")
      The second argument is used to determine whether the game should return to the map (when backToMenu is false) or to the main menu (when backToMenu is true)
      The backToMenu bool has been set to false by default, so that it's not needed to pass two arguments every time the method is called*/
    public static void MissionEnd(string result, bool backToMenu = false)
    {
        if (result == "win")
        {
            GameVariables.currentDay += 1;
            MissionWon();
            GameVariables.areasAvailable[currentMissionIndex] = false; //Mission despawns after completing it
            SceneManager.LoadScene("Overworld");
        }
        else if (result == "lose")
        {
            //Count how many missions have spawned
            var spawnedMissions = 0;
            for (int i = 0; i < GameVariables.areasLength; i++)
            {
                if (GameVariables.areasAvailable[i])
                {
                    spawnedMissions++;
                }
            }

            //This code is executed when less than the maximum amount of missions has spawned (maximum amount of missions equals "areasLength", since each area can only contain one mission)
            if (spawnedMissions < GameVariables.areasLength)
            {
                GameVariables.currentDay += 1;
                MissionLost();

                //Go to overworld map or main menu according to the value of the "backToMenu" boolean
                if (!backToMenu)
                {
                    SceneManager.LoadScene("Overworld");
                }
                else
                {
                    SceneManager.LoadScene("Main Menu");
                }
            }
            else
            {
                SceneManager.LoadScene("Game Over"); //If the maximum amount of missions have spawned (all areas are available), the player returns to the Game Over screen
            }
        }
    }

    static void MissionWon()
    {
        //After completing a mission succesfully, the difficulty and mission type of that specific mission is reset
        for (int i = 0; i < GameVariables.difficultyLevels.Length; i++)
        {
            if (currentMissionIndex == i)
            {
                GameVariables.difficultyLevels[i] = 0;
                GameVariables.typeMission[i] = "Rescue";
            }
        }
    }

    static void MissionLost()
    {
        for (int i = 0; i < GameVariables.difficultyLevels.Length; i++)
        {
            if (currentMissionIndex == i)
            {
                GameVariables.difficultyLevels[i]++; //Increase difficulty after failing a mission

                //Change mission type according to the difficulty level
                if (GameVariables.difficultyLevels[i] <= AreaBehaviour.minimumSpreadingLevel)
                {
                    GameVariables.typeMission[i] = "Kill Count";
                }
                else
                {
                    if (GameVariables.typeMission[i] == "Defend")
                    {
                        GameVariables.typeMission[i] = "Kill Count";
                    }
                    else
                    {
                        GameVariables.typeMission[i] = "Defend";
                    }
                }
            }
        }
    }

    //Resets the current scene
    public static void MissionRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Gets the index of the selected mission. This method is called in the AreaBehaviour script, when an available mission/area is clicked on
    public static void CurrentMissionIndex(int index)
    {
        currentMissionIndex = index;
    }

    //Reset variables for all mission types (rescue, killcount and defend)
    public static void ResetMissionVariables()
    {
        PeopleSavedManager.peopleSaved = 0;
        if (EnemiesKilled.KillMode)
        {
            EnemiesKilled.ResetCounter();
        }
        DefendPoint.health = 200;
    }
}

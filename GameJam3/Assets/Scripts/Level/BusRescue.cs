using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusRescue : MonoBehaviour
{
    GameObject player;
    GameObject[] civilians;
    public string missionStatus;
    Scene currentScene;
    string currentSceneName;
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentScene = SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        civilians = GameObject.FindGameObjectsWithTag("civilian");
    }

    void OnCollisionEnter2D(Collision2D otherObj)
    {
        foreach (GameObject civilian in civilians)
        {
            if (otherObj.gameObject == civilian)
            {
                Destroy(civilian);
                PeopleSavedManager.peopleSaved++;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            if (Input.GetKey(KeyCode.F))
            {
                //Show win or lose canvas depending on missionStatus variable
                //This is only executed when the current scene is not the Tutorial
                if (currentSceneName != "Tutorial")
                {
                    if (missionStatus == "win")
                    {
                        FindObjectOfType<WinLoseScreen>().ShowCanvas("win");
                    }
                    else if (missionStatus == "lose")
                    {
                        FindObjectOfType<WinLoseScreen>().ShowCanvas("bus");
                    }
                }
                else
                {
                    //If the current scene is the Tutorial and the player presses 'F' while colliding with the bus, the mission variables are reset and the mission ends
                    MissionManagementScript.ResetMissionVariables();
                    MissionManagementScript.MissionEnd("lose");
                }
            }
        }
    }

    //This method gets "win" or "lose" as a value depending on whether the winning conditions have been met or not
    public void MissionStatus(string status)
    {
        missionStatus = status;
    }
}

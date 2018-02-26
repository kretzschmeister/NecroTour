using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScreen : MonoBehaviour
{
    public GameObject winCanvas, loseCanvas, deathCanvas, busCanvas;
    GameObject[] childCanvasses;
    // Use this for initialization
    void Start()
    {
        childCanvasses = new GameObject[transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        //Get all child canvasses attached to this gameObject
        for (int i = 0; i < transform.childCount; i++)
        {
            childCanvasses[i] = transform.GetChild(i).gameObject;
            //If any of the child canvasses are active (except "BusCanvas"), the game will be paused
            if (childCanvasses[i].activeSelf && childCanvasses[i].name != "BusCanvas")
            {
                Time.timeScale = 0;
            }
        }
    }

    public void ShowCanvas(string result)
    {
        //Show correct canvas depending on result
        switch (result)
        {
            case "win":
                FindObjectOfType<PauseGame>().missionHasEnded = true;
                winCanvas.SetActive(true);
                break;
            case "lose":
                FindObjectOfType<PauseGame>().missionHasEnded = true;
                loseCanvas.SetActive(true);
                break;
            case "death":
                FindObjectOfType<PauseGame>().missionHasEnded = true;
                deathCanvas.SetActive(true);
                break;
            case "bus":
                busCanvas.SetActive(true);
                break;
        }
    }


    /*The section below contains a multiple methods
     These methods are called when clicking on buttons on the win, lose or death screens
     To see which button executes which method, click on one of the buttons in the editor
     The method it executes can be found under "On Click()" in the "Button (Script)" component*/
    public void MissionWin()
    {
        //Reset variables and end the mission (pass "win" as argument), since the button that calls this method is only visible when a mission has been completed
        MissionManagementScript.ResetMissionVariables();
        MissionManagementScript.MissionEnd("win");
    }

    public void MissionLose()
    {
        //Reset variables and end the mission (pass "lose" as argument), since the button that calls this method is only visible when a mission has been failed
        MissionManagementScript.ResetMissionVariables();
        MissionManagementScript.MissionEnd("lose");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void Cancel()
    {
        busCanvas.SetActive(false);
    }
}

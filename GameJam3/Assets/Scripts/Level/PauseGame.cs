using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    bool paused;
    public bool missionHasEnded;
    public GameObject canvas, pauseMenu, controlsMenu, exitMenu, returnText, quitText;
    public string menuToShow, buttonClicked;

    // Use this for initialization
    void Start()
    {
        menuToShow = "Pause";
    }

    // Update is called once per frame
    void Update()
    {
        //The Escape key or the 'P' key is used as a toggle to pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            menuToShow = "Pause"; //Reset the menu to show to the initial pause menu
            paused = !paused;
        }

        //Only when the mission has not ended is it possible to pauze or unpause the game
        if (!missionHasEnded)
        {
            if (paused)
            {
                canvas.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                canvas.SetActive(false);
                Time.timeScale = 1;
            }
        }

        //Show correct canvas depending on the menuToShow variable
        switch (menuToShow)
        {
            case "Pause":
                pauseMenu.SetActive(true);
                controlsMenu.SetActive(false);
                exitMenu.SetActive(false);
                break;
            case "Controls":
                pauseMenu.SetActive(false);
                controlsMenu.SetActive(true);
                exitMenu.SetActive(false);
                break;
            case "Exit":
                pauseMenu.SetActive(false);
                controlsMenu.SetActive(false);
                exitMenu.SetActive(true);

                //Show return to menu or quit game screen depending on which of the two buttons have been clicked
                switch (buttonClicked)
                {
                    case "Return":
                        returnText.SetActive(true);
                        quitText.SetActive(false);
                        break;
                    case "Quit":
                        returnText.SetActive(false);
                        quitText.SetActive(true);
                        break;
                }
                break;
        }
    }

    /*The section below contains a large number of methods
     Each visible button in the pause menu executes one of the methods below
     To see which button executes which method, click on one of the buttons in the editor
     The method it executes can be found under "On Click()" in the "Button (Script)" component*/
    public void Resume()
    {
        paused = false;
    }

    public void Controls()
    {
        menuToShow = "Controls";
    }

    public void Back()
    {
        menuToShow = "Pause";
    }

    public void ReturnToMenu()
    {
        buttonClicked = "Return";
        menuToShow = "Exit";
    }

    public void QuitGame()
    {
        buttonClicked = "Quit";
        menuToShow = "Exit";
    }

    public void YesButton()
    {
        switch (buttonClicked)
        {
            case "Return":
                MissionManagementScript.ResetMissionVariables();
                MissionManagementScript.MissionEnd("lose", true);
                break;
            case "Quit":
                Application.Quit();
                break;
        }
    }

    public void NoButton()
    {
        menuToShow = "Pause";
    }
}

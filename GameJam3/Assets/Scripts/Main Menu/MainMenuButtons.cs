using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject initialCanvas, difficultyCanvas, confirmationCanvas;
    GameObject normalButton, hardButton, normalModeButton, realismModeButton;
    public Color selectedNormalColor, selectedHighlighedColor, selectedPressedColor, unavailableColor;
    string showCanvas;
    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 1;
        showCanvas = "Initial"; //Show "Initial" canvas on Awake
    }

    // Update is called once per frame
    void Update()
    {
        //Show correct canvas depending on the showCanvas variable
        switch (showCanvas)
        {
            case "Initial":
                initialCanvas.SetActive(true);
                difficultyCanvas.SetActive(false);
                confirmationCanvas.SetActive(false);
                break;
            case "Difficulty":
                initialCanvas.SetActive(false);
                difficultyCanvas.SetActive(true);
                confirmationCanvas.SetActive(false);
                break;
            case "Confirmation":
                initialCanvas.SetActive(false);
                difficultyCanvas.SetActive(false);
                confirmationCanvas.SetActive(true);
                break;
        }
    }

    /*The section below contains a large number of methods
      Each visible button in the main menu executes one of the methods below
      To see which button executes which method, click on one of the buttons in the editor
      The method it executes can be found under "On Click()" in the "Button (Script)" component*/

    public void NewGame()
    {
        PlayerHealthManager.ResetVariables();
        showCanvas = "Difficulty";
    }

    public void ContinueGame()
    {
        if (GameVariables.startedGame)
        {
            SceneManager.LoadScene("Overworld");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        showCanvas = "Initial";
    }

    public void NormalDifficulty()
    {
        GameVariables.hardModeMultiplier = 1;
    }

    public void HardDifficulty()
    {
        GameVariables.hardModeMultiplier = 2;
    }

    public void StandardMode()
    {
        GameVariables.realisticMode = false;
    }

    public void RealisticMode()
    {
        GameVariables.realisticMode = true;
    }

    public void StartGame()
    {
        if (GameVariables.playTutorial)
        {
            GameVariables.startedGame = true;
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            showCanvas = "Confirmation";
        }
    }

    public void PlayTutorial()
    {
        GameVariables.playTutorial = true;
    }

    public void DontPlayTutorial()
    {
        GameVariables.playTutorial = false;
    }

    public void YesConfirmation()
    {
        GameVariables.startedGame = true;
        SceneManager.LoadScene("Overworld");
    }

    public void NoConfirmation()
    {
        showCanvas = "Difficulty";
    }
}

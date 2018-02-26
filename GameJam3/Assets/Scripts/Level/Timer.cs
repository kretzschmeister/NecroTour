using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static bool timeRunOut = false;
    public float maxTime = 10;
    public float interval = 2;
    public static float timeLeft;
    float time;
    public GameObject textTime;
    Text timeText;
    private void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
        timeText = GetComponent<Text>();
        timeLeft = maxTime;

    }

    void FixedUpdate()
    {
        time = Time.deltaTime;
        timeLeft -= time;
        timeText.color = Color.white;
        timeText.text = "" + Mathf.Round(timeLeft);
        if (timeLeft <= 0)
        {
            timeRunOut = true;
            timeText.fontSize = 32;
            timeText.color = Color.red;
            timeText.text = "More zombies incoming!";
            if (textTime != null)
                textTime.SetActive(false);
        }
    }
}

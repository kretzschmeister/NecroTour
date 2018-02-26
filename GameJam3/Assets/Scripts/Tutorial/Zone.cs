using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    // checks if player has reached the zone and allows zombies in that zone to move
    public bool inZone = false;
    // checks if all zombies have been counted
    public bool checkedZombies = false;
    // Hides text of canvas in tutorialstates
    public static bool stateActivated = false;
    // called in survivor pathfinding and changes the range when he should move
    public static bool changeRange = true;
    // the state which belongs to the zone
    public string stateName;
    public int numberOfZombies = 0;
    GameObject player;
    GameObject[] enemies;
    // Use this for initialization
    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numberOfZombies = 0;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TutorialStates.stateName == stateName && checkedZombies == true) {
            TutorialStates.nZombies = numberOfZombies;
            checkedZombies = false;
            
        }
    }
   
    private void OnTriggerEnter2D(Collider2D otherObj)
    {
        // Checks which zombies are in the area and counts the amount of zombies
        if (otherObj.gameObject.tag == "Enemy")
        {
            otherObj.GetComponent<EnemyBehaviour>().tutorialZombie = true;
            Debug.Log("enter " + stateName);
            checkedZombies = true;
            numberOfZombies++;
        }
        //Activates the state at Tutorial
        if (otherObj.gameObject.tag == "Player")
        {
            inZone = true;
            stateActivated = true;
            changeRange = true;
            if (stateName !=null)
            TutorialStates.stateName = stateName;
        }
        
    }
   
    private void OnTriggerStay2D(Collider2D otherObj)
    {
        //Actions based on what the other objects are
        if (otherObj.gameObject.tag == "Enemy")
        {

            if (inZone)
            {               
                otherObj.gameObject.GetComponent<EnemyPathfinding>().rotationIn2D = true;
                otherObj.gameObject.GetComponent<EnemyPathfinding>().target = player.transform;
            }
            else
            {
                otherObj.gameObject.GetComponent<EnemyPathfinding>().rotationIn2D = false;
            }            
        }
             
    }
}
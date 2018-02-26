using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject Blood;
    public bool tutorialZombie = false;
    public int health = 2;
    //pickup variables
    [Header("Pick Ups")]
    public GameObject healthPickUp;
    public GameObject pistolAmmoPickUp;
    public GameObject shotgunAmmoPickUp;
    public GameObject machinegunAmmoPickUp;




    Quaternion rot;
    public static float rangeTarget, chaseRange;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rangeTarget = 100;

    }

    //Draws gizmos to comprehend the zombie movement better
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, rangeTarget);
    }

    private void FixedUpdate()
    {

        if (health <= 0)
        {
            //random pickup drops
            RandomPickUp();
            if (tutorialZombie) TutorialStates.nZombies--;

            Instantiate(Blood, transform.position, transform.rotation);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("ZombieDead");

            // Drops the counter by one in kill count
            if (EnemiesKilled.KillMode && FindObjectOfType<EnemiesKilled>() != null)
            {
                FindObjectOfType<EnemiesKilled>().EnemyKilled();
            }
        }
    }


    //method die zorgt voor een random drop van pickups. 
    void RandomPickUp()
    {

        int random = Random.Range(0, 100);


        //smerige code voor de pick up drops.


        if (random <= 10)
        {
            Instantiate(healthPickUp, transform.position, transform.rotation);
        }
        else if (random <= 15)
        {
            Instantiate(shotgunAmmoPickUp, transform.position, transform.rotation);
        }
        else if (random <= 20)
        {
            Instantiate(machinegunAmmoPickUp, transform.position, transform.rotation);
        }
        else if (random <= 30)
        {
            Instantiate(pistolAmmoPickUp, transform.position, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour {

    GameObject Gun,  GunBarrel;
	Rigidbody2D myRB;
    Vector2 direction;
    // eigenschappen van de bullets
    public float rocketSpeed;
    public int damage;
    int i;

	//awake starts as soon as object comes to life. 
	void Awake () {
       
        myRB = GetComponent<Rigidbody2D>();

        // Richting bepalen van de bullet
        Gun = GameObject.FindGameObjectWithTag("Gun");
        GunBarrel = GameObject.FindGameObjectWithTag("GunBarrel");
        direction = GunBarrel.transform.position -Gun.transform.position;
        direction.Normalize();
		myRB.AddForce (direction * rocketSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      

    }
    void OnCollisionEnter2D(Collision2D otherObj)
    {
        //Actions based on what the other objects are
        switch (otherObj.gameObject.tag)
        {
            case "Wall":
                {
                    Destroy(gameObject);
                    break;
                }
            case "Bus":
                {
                    Destroy(gameObject);
                    break;
                }
            case "Enemy":
                {
                    otherObj.gameObject.GetComponent<EnemyBehaviour>().health -= damage;
                    Destroy(gameObject);
                    break;
                }
            case "civilian":
                {
                    if(GameVariables.realisticMode)
                        otherObj.gameObject.GetComponent<CivilianMovement>().health -= damage;
                    Destroy(gameObject);
                    break;
                }
            case "Survivor":
                {
                    if (GameVariables.realisticMode)
                        otherObj.gameObject.GetComponent<SurvivorMovement>().health -= damage;
                    Destroy(gameObject);
                    break;
                }
            case "Box":
                {
                    otherObj.gameObject.GetComponent<BreakableBox>().boxHealth -= otherObj.gameObject.GetComponent<BreakableBox>().damageAmountBullet;
                    Destroy(gameObject);
                    break;
                }
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BreakableBox : MonoBehaviour
{

    
    //Health of box
    public int boxHealth;
    public int maxBoxHealth;
    // the values that changes the sprite of the box
    public int healthTreshold1;
    public int healthTreshold2;
    // Damage dealt to the box from different objects
    public int damageAmountEnemy, damageAmountBullet, damageAmountMelee;
    // Time where the box is immune to damage, so it doesn't break too soon
    public float damageTime, nDamageTime;

    //pickup variables
    public GameObject certainPickUp;
    public GameObject healthPickUp;
    public GameObject pistolAmmoPickUp;
    public GameObject shotgunAmmoPickUp;
    public GameObject machinegunAmmoPickUp;
    // Drop chance percentage
    public int dropChanceHealth = 10;
    public int dropChanceShotgunAmmo = 15;
    public int dropChanceMachinegunAmmo = 20;
    public int dropChancePistolAmmo = 30;
    //sprite variables 
    public Sprite spriteBox;
    public Sprite spriteBoxHalfBroken;
    public Sprite spriteBoxAlmostBroken;
    public Sprite spriteBoxBroken;

    bool collide = true;
    Collider2D c;
    public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        boxHealth = maxBoxHealth;
        damageTime = nDamageTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = spriteBoxBroken;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (boxHealth > maxBoxHealth)
        {
            boxHealth = maxBoxHealth;
        }
        else if (boxHealth <= 0)
        {
            boxHealth = 0;
        }
        // BoxHealth determines the sprite
        if (boxHealth > healthTreshold1)
        {
            spriteRenderer.sprite = spriteBox;
        }
        else if (boxHealth > healthTreshold2)
        {
            spriteRenderer.sprite = spriteBoxHalfBroken;
        }
        else if (boxHealth > 0)
        {
            spriteRenderer.sprite = spriteBoxAlmostBroken;
        }
        else if (boxHealth <= 0)
        {
            spriteRenderer.sprite = spriteBoxBroken;
            if (collide)
            {
                c.enabled = !c.enabled;
                collide = false;
                RandomPickUp();

                if (certainPickUp != null) Instantiate(certainPickUp, transform.position, transform.rotation);       
            }
        }
    }

    void RandomPickUp()
    {
        int random = Random.Range(0, 100);
        if (random <= dropChanceHealth)
        {
            Instantiate(healthPickUp, transform.position, transform.rotation);
        }
        else if (random <= dropChanceShotgunAmmo)
        {
            Instantiate(shotgunAmmoPickUp, transform.position, transform.rotation);
        }
        else if (random <= dropChanceMachinegunAmmo)
        {
            Instantiate(machinegunAmmoPickUp, transform.position, transform.rotation);
        }
        else if (random <= dropChancePistolAmmo)
        {
            Instantiate(pistolAmmoPickUp, transform.position, transform.rotation);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            damageTime -= Time.deltaTime;
            if (damageTime <= 0)
            {
                boxHealth -= damageAmountEnemy;
                damageTime = nDamageTime;
            }
        }
    }
}

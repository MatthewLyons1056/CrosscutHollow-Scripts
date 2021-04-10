using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Score score;
    public DropStuffOnDeath lootDrop;
    

    //health

    public int health = 100;

    //Mana
    public ResourceBar rBar;

    //Death effects

    public GameObject deathEffect; //insert death animation 

    ///animation section
    ///

    //public Animator animator;



    private void Start()
    {

        lootDrop = GetComponent<DropStuffOnDeath>();

    }

    void Update()
    {
        //animator.SetFloat("Speed", Mathf.Abs());
    }

    public void TakeDamage(int damage)
    {

        Debug.Log("taking " + damage + " from health of " + health);
        health -= damage;

        if (health <= 0)
        {
            Debug.Log(health);
            Die();
        }
        
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //score.AddScore(1);
        //Play a sound on death
        lootDrop.DropOnDeath();
    }

}

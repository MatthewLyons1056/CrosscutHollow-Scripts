using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MeleeAttack : MonoBehaviour
{

    public Vector2[] topAttack = new Vector2[2] { new Vector2(), new Vector2() }; // the collider ray for the upper attack
    public Vector2[] midAttack = new Vector2[2] { new Vector2(), new Vector2() }; // the collider ray for the middle attack
    public Vector2[] lowAttack = new Vector2[2] { new Vector2(), new Vector2() }; // the collider ray for the low attack
    public float attackDelay = 1.0f; // the cooldown between consecutive attacks
    public int playerDamage = 40; // the melee damage the player deals on contact

    private float nextAttack = -1.0f;
    //reference animatorr
    public Animator animator;
    //reference melee audio
    public AudioSource MeleeSound;

    private float knockbackMult = 0.4f;
    public void flip() // called when the player turns around
    {

        topAttack[0][0] *= -1.0f;
        topAttack[1][0] *= -1.0f;
        midAttack[0][0] *= -1.0f;
        midAttack[1][0] *= -1.0f;
        lowAttack[0][0] *= -1.0f;
        lowAttack[1][0] *= -1.0f;

    }

    private void OnDrawGizmos() // draws the rays for debugging purposes
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine((Vector2)transform.position + topAttack[0], (Vector2)transform.position + topAttack[1]);
        Gizmos.DrawLine((Vector2)transform.position + midAttack[0], (Vector2)transform.position + midAttack[1]);
        Gizmos.DrawLine((Vector2)transform.position + lowAttack[0], (Vector2)transform.position + lowAttack[1]);

    }

    private void checkCollisions(RaycastHit2D[] cols) // iterates through all the hits to check for enemies
    {

        foreach (RaycastHit2D col in cols)
        {

            if (col.transform.gameObject.CompareTag("Enemy"))
            {

                Debug.Log("dealing " + playerDamage + " to " + col.transform.gameObject.name);
                col.transform.gameObject.GetComponent<Enemy>().TakeDamage(playerDamage);
                col.transform.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2)(col.transform.position - transform.position).normalized * knockbackMult);
                break;
                
            }

        }

    }

    void Update()
    {

        if (Input.GetButtonDown("Fire2") && Time.time >= nextAttack)
        {

            attackCheck();
            

        }
        //reset animation series of bools
        else 
        {
            animator.SetBool("IsMelee", false);
            animator.SetBool("IsMeleeUp", false);
            animator.SetBool("IsMeleeDown", false);
        }
    }

    private void attackCheck()
    {

        nextAttack = Time.time + attackDelay;
        if (Input.GetAxisRaw("Vertical") > 0)
        {

            Debug.Log("calling attack up");
            attackUp();
            MeleeSound.Play();

        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {

            Debug.Log("calling attack norm");
            attackNorm();
            MeleeSound.Play();
        }
        else
        {

            Debug.Log("calling attack down");
            attackDown();
            MeleeSound.Play();
        }

    }

    public void attackUp()
    {
        animator.SetBool("IsMeleeUp", true);
        checkCollisions(Physics2D.LinecastAll((Vector2)transform.position + topAttack[0], (Vector2)transform.position + topAttack[1]));
        Debug.Log("uppercut detected");

    }

    public void attackNorm()
    {
        animator.SetBool("IsMelee", true);
        checkCollisions(Physics2D.LinecastAll((Vector2)transform.position + midAttack[0], (Vector2)transform.position + midAttack[1]));
        Debug.Log("punch detected");

    }

    public void attackDown()
    {
        animator.SetBool("IsMeleeDown", true);
        checkCollisions(Physics2D.LinecastAll((Vector2)transform.position + lowAttack[0], (Vector2)transform.position + lowAttack[1]));
        Debug.Log("kick detected");

    }
}

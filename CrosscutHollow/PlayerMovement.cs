using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Ref other scripts
    public CharacterController2D controller;
    public Animator animator;

    //refer to ladder climb
    
    float horizontalMove = 0f;

    public float runSpeed = 40f;

    bool jump = false;
    bool cast = false;
    bool ignoreInput = false;


    //sound section

    public AudioSource landingSound;

    void Update()
    {

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (!ignoreInput)
        {

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            //Jump Cast
            if (Input.GetButtonDown("Jump"))
            {

                jump = true;
                animator.SetBool("IsJumping", true);

            }
            if (Input.GetKey(KeyCode.W) & Input.GetButtonDown("Fire1"))
            {
                

                animator.SetBool("IsShootingUp", true);
                
            }
            if (Input.GetKey(KeyCode.S) & Input.GetButtonDown("Fire1"))
            {


                animator.SetBool("IsShootingDown", true);
                
            }

            if (Input.GetButtonDown("Fire1"))
            {

                cast = true;
                animator.SetBool("IsCasting", true);

            }
            
            else
            {
                //stop casting
                animator.SetBool("IsCasting", false);
                animator.SetBool("IsShootingUp", false);
                animator.SetBool("IsShootingDown", false);



            }

        }

    }

    public bool getJump()
    {

        return jump;

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        landingSound.Play();
    }

    public void setIgnoreInput(bool ig)
    {

        ignoreInput = ig;

    }

    public void FinishCast()
    {
        animator.SetBool("IsCasting", false);
    }
    void FixedUpdate()
    {
        //Move Character in reference
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;        
    }
}

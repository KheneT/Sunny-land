using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //Variables
    public float runSpeed = 2;

    public float jumpSpeed = 3;

    public float doubleJumpSpeed = 5.5f;

    private bool canDoubleJump;

    public bool betterJump = false;

    public float fallMultiplier = 0.5f;

    public float lowJumpMultiplier = 1f;

    //Component References
    Rigidbody2D myRigidBody2D;

    SpriteRenderer mySpriteRenderer;

    public Animator myAnimator;
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey("space"))
        {
            if (CheckGround.isGrounded)
            {
                canDoubleJump = false;
                myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpSpeed);
            }
            else if (Input.GetKeyDown("space"))
            {
                if (canDoubleJump)
                {
                    
                    myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, doubleJumpSpeed);
                    canDoubleJump = false;
                }
            }
        }

        if (CheckGround.isGrounded == false)
        {
            myAnimator.SetBool("Jump", true);
            myAnimator.SetBool("Run", false);
        }
        else if (CheckGround.isGrounded == true)
        {
            myAnimator.SetBool("Jump", false);
            
            myAnimator.SetBool("Fall", false);
        }

        if (myRigidBody2D.velocity.y < 0)
        {
            myAnimator.SetBool("Fall", true);
        }
        else if (myRigidBody2D.velocity.y > 0)
        {
            myAnimator.SetBool("Fall", false);
        }

    }

    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            myRigidBody2D.velocity = new Vector2(runSpeed, myRigidBody2D.velocity.y);
            mySpriteRenderer.flipX = false;
            myAnimator.SetBool("Run", true);
        }
        else if (Input.GetKey("a"))
        {
            myRigidBody2D.velocity = new Vector2(-runSpeed, myRigidBody2D.velocity.y);
            mySpriteRenderer.flipX = true;
            myAnimator.SetBool("Run", true);
        }
        else
        {
            myRigidBody2D.velocity = new Vector2(0, myRigidBody2D.velocity.y);
            myAnimator.SetBool("Run", false);
        }

        if (betterJump)
        {
            if (myRigidBody2D.velocity.y < 0)
            {
                myRigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            else if (myRigidBody2D.velocity.y > 0 && !Input.GetKey("space"))
            {
                myRigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }

    }
}



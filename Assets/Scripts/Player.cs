using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    List<GameObject> children;
    GameObject child;
    Health health;
    EnemyMovement enemyMovement;
    Tutorial tutorial;
    [SerializeField] CinemachineVirtualCamera followCamera;
    GameObject tutoriaImage;
    float distance;
    bool isAlive = true;
    bool isEnemy;
    bool canMove = true;
    bool endedTutorial = false;
    void Start()
    {
        child = transform.Find("PlayerColliders").gameObject;
        tutoriaImage = GameObject.FindWithTag("TutorialImage");
        tutorial = FindObjectOfType<Tutorial>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        health = GetComponent<Health>();
        myBodyCollider = child.GetComponent<CapsuleCollider2D>();
        myFeetCollider = child.GetComponent<BoxCollider2D>();
    }
    public CapsuleCollider2D GetBodyCollider()
    {
        return myBodyCollider;
    }

    public GameObject GetChild()
    {
        return child;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        if (canMove)
        {
            Run();
            FlipSprite();
            TouchingGrass();
            Die();
        }
        if (endedTutorial)
        {
            Vector2 velocity = new Vector2(2, myRigidbody.velocity.y);
            Debug.Log(velocity);
            myRigidbody.velocity = velocity;
        }
    }
    void DisableControls()
    {
        canMove = false;
    }

    void Die()
    {
        int HP = health.GetHealth();
        if (HP <= 0)
        {
            myAnimator.SetBool("isDead", true);
            isAlive = false;
            DisableControls();
        }
    }

    void FlipSprite()
    {
        bool hasSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (hasSpeed)
        {
            transform.localScale = new Vector2((Mathf.Sign(myRigidbody.velocity.x) * -1.75f), 1.75f);
        }
    }

    void OnMove(InputValue value)
    {
        tutorial.hasMoved = true;
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool hasSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isWalking", hasSpeed);
    }
    void OnJump(InputValue value)
    {
        tutorial.hasJumped = true;
        if (!isAlive)
        {
            return;
        }
        bool hasJumped = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        // Debug.Log("liftoff!!!!!!!!!");
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Bouncer")))
        {
            return;
        }
        if (value.isPressed)
        {

            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

    }
    void TouchingGrass()
    {
        // Debug.Log(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")));
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Bouncer")))
        {
            myAnimator.SetBool("isJumping", true);
        }

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Bouncer")))
        {
            myAnimator.SetBool("isJumping", false);
        }
    }

    IEnumerator OffGround()
    {
        yield return new WaitForSeconds(1f);
    }
    void OnFire()
    {
        tutorial.hasAttacked = true;
        if (!isAlive)
        {
            return;
        }
        Instantiate(bullet, gun.position, transform.rotation);
        // DamageDealer damageDealer = GetComponent<DamageDealer>();
        myAnimator.SetBool("IsAttacking", true);
        // Debug.Log("Starting Animation");
        // if (isEnemy)
        // {
        //     damageDealer.Hit();
        // }
        StartCoroutine(EndAnim());

    }

    IEnumerator EndAnim()
    {
        yield return new WaitForSeconds(0);
        myAnimator.SetBool("IsAttacking", false);
        // Debug.Log("Ending Animation");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnterTrigger")
        {
            DisableControls();
            followCamera.m_Follow = null;
            Debug.Log("No Follow Camera");
            endedTutorial = true;
        }
        if (other.tag == "End Trigger")
        {
            tutoriaImage.Image.Color()
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    float distance;
    public bool flip;
    bool isAlive = true;
    // bool isMovingBack = false;


    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float fireDistance = 2f;
    float originalMoveSpeed;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Health myHealth;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFootCollider;
    GameObject child;
    GameObject body;
    Coroutine firingCoroutine;
    public bool isFiring;
    bool withinDistance;
    // bool useAI = true;
    void Start()
    {
        child = transform.Find("EnemyBoxCollider").gameObject;
        body = transform.Find("EnemyBodyCollider").gameObject;
        myBodyCollider = gameObject.GetComponent<CapsuleCollider2D>();
        myFootCollider = child.gameObject.GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        isFiring = true;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        IsMoving();
        Debug.Log(isAlive);
        if (distance < 5 && isAlive)
        {
            Debug.Log("Anything");
            Attackkkkkk();
            FollowPlayer();
            // Debug.Log("Following Player");
        }
    }
    public float GetDistance()
    {
        return distance;
    }

    void Attackkkkkk()
    {
        // if (!isAlive)
        // {
        //     return;
        // }
        DamageDealer damageDealer = GetComponent<DamageDealer>();
        if (distance < fireDistance && firingCoroutine == null && isAlive)
        {

            firingCoroutine = StartCoroutine(WaitBetweenAttacks());
        }
        else if (distance > fireDistance && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        if (!isAlive)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator WaitBetweenAttacks()
    {
        while (isAlive)
        {
            Instantiate(bullet, this.transform);
            // Debug.Log("this transform: " + this.transform);
            // myHealth.TakeDamage(damageDealer.Getmage());
            myAnimator.SetBool("isAttacking", true);
            myAnimator.SetBool("isAttacking", false);


            yield return new WaitForSeconds(fireRate);
            yield return new WaitForEndOfFrame();

        }

    }

    // void Move()
    // {
    //     if (!isAlive || isMovingBack)
    //     {
    //         return;
    //     }
    //     if (transform.localScale.x < 0 && moveSpeed > 0)
    //     {
    //         FlipEnemyFacing();
    //     }
    //     if (transform.localScale.x > 0 && moveSpeed < 0)
    //     {
    //         FlipEnemyFacing();
    //     }
    //     if (!isTriggered)
    //     {

    //     }
    //     myRigidBody.velocity = new Vector2 (moveSpeed, 0f);
    //     myAnimator.SetBool("IsWalking", true);
    //     Debug.Log(myRigidBody.velocity);
    // }

    void FollowPlayer()
    {
        // if (!isAlive)
        // {
        //     // Debug.Log("Returning");
        //     return;
        // }
        originalMoveSpeed = moveSpeed;
        Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            // Debug.Log(scale.x);
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
        }
        myAnimator.SetBool("IsWalking", true);
        transform.localScale = scale;
    }

    void IsMoving()
    {
        bool hasSpeed = Mathf.Abs(myRigidBody.velocity.x) < Mathf.Epsilon;
        if (Mathf.Abs(myRigidBody.velocity.x) < Mathf.Epsilon)
        {
            // isMovingBack = true;
            myAnimator.SetBool("IsWalking", false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bite" && distance < 2)
        {


            if (myHealth.GetHealth() < Mathf.Epsilon)
            {
                isAlive = false;
                myAnimator.SetTrigger("isDead");
                Destroy(myBodyCollider);
                Destroy(myFootCollider);
            }
        }
        if (other.tag == "Ground" && gameObject.name == "EnemyBoxCollider")
        {
            moveSpeed = originalMoveSpeed;
            // isMovingBack = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground" && gameObject.name == "EnemyBoxCollider")
        {
            // originalMoveSpeed = moveSpeed;
            moveSpeed = 0;

        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * -1.75f, 1.75f);
        // Debug.Log("Flipped Sprite");

        // StartCoroutine(MoveBack());
        // Debug.Log("Moved back");
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log("Collided");
        }

    }

    // IEnumerator MoveBack()
    // {
    // transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    // Debug.Log("Moving back");
    // Debug.Log(moveSpeed * Time.deltaTime);
    // yield return new WaitForSeconds(10);
    // isMovingBack = false;
    // }
}

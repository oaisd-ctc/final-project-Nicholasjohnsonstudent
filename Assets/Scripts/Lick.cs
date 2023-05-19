using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lick : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    // [SerializeField] List<GameObject> enemys;
    // GameObject enemy;
    GameObject myEnemy;
    GameObject player;
    // Player player;
    Health health;
    DamageDealer damageDealer;
    EnemyMovement enemyMovement;
    Rigidbody2D myRigidbody;
    // Vector3 enemyGameObject;
    // List<float> enemyDistances;
    float distance;
    float playerDistance;
    // float enemyDistance;.
    float xSpeed;
    // int I;
    [SerializeField] float projectileDistance = 2f;

    void Awake() 
    {
        myEnemy = GetComponentInParent<Rigidbody2D>().gameObject;
        Debug.Log("Enemy: " + myEnemy);
    }
    void Start()
    {
        
        // enemy = GetComponent<EnemyMovement>().gameObject;
        // foreach (GameObject enemy in enemys)
        // {
        //     enemyMovement = enemy.GetComponent<EnemyMovement>();
        //     enemyDistance = Vector2.Distance(transform.position, enemy.transform.position);
        //     enemyDistances[I] = enemyDistance;
        //     I++;
        //     Debug.Log("array thingy: " + I);
        // }
        
        // enemyGameObject = myRigidbody.gameObject.transform.position;
        transform.parent = null;
        myRigidbody = myEnemy.GetComponent<Rigidbody2D>();
        
        // transform.localScale = new Vector2(1, 1);
        player = FindObjectOfType<Player>().gameObject;
        damageDealer = this.GetComponent<DamageDealer>();
        health = player.GetComponent<Health>();
        // health.TakeDamage(damageDealer.GetDamage());
        // player = FindObjectOfType<Player>();
        xSpeed = (gameObject.transform.localScale.x * bulletSpeed);
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, myRigidbody.gameObject.transform.position);
        playerDistance = Vector2.Distance(player.transform.position, myRigidbody.gameObject.transform.position);
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
        // Debug.Log(transform.position);
        // Debug.Log(myRigidbody.gameObject.transform.position);
        // Debug.Log("Distance: " + distance);
        // Debug.Log("Projectile Distance: " + projectileDistance);
        if (distance > projectileDistance)
        {
            Debug.Log("destroyed bullet");
            Destroy(gameObject);
        }

        // if (playerDistance < 2.5f)
        // {
        //     // Debug.Log("Enemy bullet is within bounds");
        //     health.TakeDamage(damageDealer.GetDamage());
        //     Destroy(gameObject);
        // }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            // health = other.GetComponent<Health>();
            health.TakeDamage(damageDealer.GetDamage());
        }
    }
    // void OnTriggerExit2D(Collider2D other) 
    // {
    //     if (other.tag == "Enemy")
    //     {
    //         myRigidbody = other.GetComponent<Rigidbody2D>();
    //     }
    // }

    // IEnumerator Firing()
    // {
    //     yield return new WaitForSeconds(projectileLifetime);
    // }
}

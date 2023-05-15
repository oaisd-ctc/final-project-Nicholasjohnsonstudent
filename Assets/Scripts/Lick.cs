using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lick : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] GameObject enemy;
    // GameObject enemy;
    Rigidbody2D myRigidbody;
    GameObject player;
    // Player player;
    Health health;
    DamageDealer damageDealer;
    float distance;
    float playerDistance;
    float xSpeed;
    [SerializeField] float projectileDistance = 2f;

    void Start()
    {
        
        // enemy = GetComponent<EnemyMovement>().gameObject;
        myRigidbody = this.GetComponent<Rigidbody2D>();
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
        // Debug.Log(playerDistance);
        Debug.Log(myRigidbody.gameObject.transform.localScale.x);
        if (distance > projectileDistance)
        {
            Destroy(gameObject);
        }

        // if (playerDistance < 2.5f)
        // {
        //     Debug.Log("Enemy bullet is within bounds");
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

    // IEnumerator Firing()
    // {
    //     yield return new WaitForSeconds(projectileLifetime);
    // }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    Player player;
    EnemyMovement enemy;
    Health health;
    float distance;
    float xSpeed;
    [SerializeField] float projectileDistance = 2f;
    GameObject enemyChild;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        xSpeed = (player.transform.localScale.x * bulletSpeed) * -1;
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
        if (distance > projectileDistance)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.tag == "Enemy")
        {
            Debug.Log("Tag: " + other);
            // enemyChild = other.transform.Find("EnemyBoxCollider").gameObject;
            DamageDealer damageDealer = this.GetComponent<DamageDealer>();
            health = other.GetComponent<Health>();
            health.TakeDamage(damageDealer.GetDamage());
        }
    }

    // IEnumerator Firing()
    // {
    //     yield return new WaitForSeconds(projectileLifetime);
    // }
}

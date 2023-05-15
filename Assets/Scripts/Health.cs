using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 6;
    Player player;
    EnemyMovement enemyMovement;
    
    float distance;
    void Start()
    {
        player = GetComponent<Player>();
        
    }
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        enemyMovement = other.GetComponent<EnemyMovement>();
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            
            TakeDamage(damageDealer.GetDamage());
            // if (other.tag == "Lick")
            // {
            //     TakeDamage(damageDealer.GetDamage());
            // }
            // if (other.tag == "Bite")
            // {

            //     TakeDamage(damageDealer.GetDamage());
            // }
            
        }
    }
    // void OnTriggerExit2D(Collider2D other)
    // {
        // DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        // if (other.tag == "Lick" && damageDealer != null)
        // {
        //     TakeDamage(damageDealer.GetDamage());
        // }
    // }
    public void TakeDamage(int damage)
    {

        health -= damage;
        // if (health <= 0)
        // {

        // }
    }
    public int GetHealth()
    {
        return health;
    }
}

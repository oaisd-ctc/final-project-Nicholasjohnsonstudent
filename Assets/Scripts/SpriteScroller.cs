using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] Player player;
    [SerializeField] float scrollSpeed;
    Vector2 offset;
    Material material;
    Rigidbody2D myRigidbody;
    // GameObject child;

    void Start()
    {
        // child = transform.Find("PlayerColliders").gameObject;
        myRigidbody = player.GetComponent<Rigidbody2D>();
    }
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    
    void Update()
    {
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
        FlipDirection();
    }

    void FlipDirection()
    {
        Vector2 reference = new Vector2(Mathf.Epsilon, Mathf.Epsilon);
        bool hasSpeed = myRigidbody.velocity.x < 0;
        // Debug.Log("Has Speed: " + hasSpeed);
        // Debug.Log("Move Speed: " + moveSpeed.x);
        if (hasSpeed && moveSpeed.x >= 0)
        {
            moveSpeed.x = scrollSpeed * -1;
        }
        else if (!hasSpeed && moveSpeed.x <= 0)
        {
            moveSpeed.x = scrollSpeed;
        }
        // Debug.Log("Move Speed: " + moveSpeed.x);
        // Debug.Log("Player Velocity: " + myRigidbody.velocity.x);
        if(myRigidbody.velocity.x == 0)
        {
            moveSpeed.x = 0;
        }
    }
}

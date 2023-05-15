using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float seconds = 1;
    Animator bounceAnimator;
    // Start is called before the first frame update
    void Start()
    {
        bounceAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject == player)
        {
            StartCoroutine(PlayAnimation());
            bounceAnimator.SetBool("IsTouched", true);
        }
    }

    IEnumerator PlayAnimation()
    {
        bounceAnimator.SetBool("IsTouched", true);
        yield return new WaitForSeconds(seconds);
        bounceAnimator.SetBool("IsTouched", false);
    }
}

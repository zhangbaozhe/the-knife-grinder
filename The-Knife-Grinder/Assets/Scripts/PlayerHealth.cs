using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator animator;
    private int health = 100;
    private Rigidbody rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "fist")
        {
            animator.Play("get_hit");
            health = health - 8;
            rb.AddExplosionForce(1.0f, collision.transform.position, 2.0f);
        }
        else if(collision.collider.tag == "foot")
        {
            animator.Play("get_hit");
            health = health - 13;
            rb.AddExplosionForce(1.6f, collision.transform.position, 3.0f);
        }
        else if(collision.collider.tag == "knife")
        {
            animator.Play("get_hit");
            health = health - 22;
        }
    }
}

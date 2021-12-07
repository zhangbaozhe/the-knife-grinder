using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth _instance;
    private Animator animator;
    public int health = 100;
    public bool isdead = false;
    private Rigidbody rb;
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void dead()
    {
        animator.Play("dead");
        isdead = true;
    } 

    private void OnCollisionEnter(Collision collision)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("punch") || 
            animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing"))
        {
            return;
        }
        if(collision.collider.tag == "fist")
        {
            rb.AddExplosionForce(10000.0f, collision.transform.position, 1.0f);
            animator.Play("get_hit");
            health = health - 8;
        }
        else if(collision.collider.tag == "foot")
        {
            rb.AddExplosionForce(15000f, collision.transform.position, 2.0f);
            animator.Play("get_hit");
            health = health - 13;
        }
        else if(collision.collider.tag == "knife")
        {
            animator.Play("get_hit");
            health = health - 22;
        }
        if(health <= 0)
        {
            dead();
        }
    }



}

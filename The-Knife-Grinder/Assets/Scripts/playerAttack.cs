using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    private Animator animator;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        attack();

    }
    void Delay(int t)
    {
        for(int i = t; i>=0; i--)
        {
            ;
        }
    }
    private void attack()
    {

        if (transform.position.y >= -0.596f)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            if (rb.velocity.magnitude > 4 && !animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
            {
                animator.Play("flying_kick");
            }

            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("punch") && !animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick"))
            { 
                animator.Play("punch");
            }

        }


    }


}

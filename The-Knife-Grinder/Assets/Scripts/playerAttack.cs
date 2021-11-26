using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

        
        if (Input.GetMouseButtonDown(1) && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Debug.Log("attack");
            //animator.SetBool("attack", true);
            //Delay(int.MaxValue);
            //animator.SetBool("attack", false);
            animator.Play("punch");
        }
        

    }
}

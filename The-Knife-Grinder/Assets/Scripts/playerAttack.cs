﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerAttack : NetworkBehaviour
{

    private Animator animator;
    private Rigidbody rb;

    private float coolingTimer = 0.6f;
    private float currentTime = 0.0f;

    public Transform detector;
    public LayerMask lm;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }

        if(Input.GetMouseButtonDown(1) && !PlayerHealth._instance.isdead)
            attack();
        Delay();
    }

    void Delay()
    {
        if (currentTime < coolingTimer)
        {
            currentTime += Time.deltaTime;
        }
    }

    private void attack()
    {

        if (!Physics.Raycast(detector.position, Vector3.down, 0.2f, lm))
        {
            Debug.Log("return--inair");
            return;
        }
        if (currentTime < coolingTimer)
            return;
        
            if (rb.velocity.magnitude >=6 && !animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
            {
                animator.Play("flying_kick");
                currentTime = -1.1f;
            }

        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick"))
        { 
            if(gameManager._instance.isFinalStage && !animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing"))
            {
                animator.Play("stabbing");
                currentTime = 0.0f;
            }
            if (!gameManager._instance.isFinalStage && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
            {
                animator.Play("punch");
                currentTime = 0.0f;
            }
        }
        
    }

}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerAttack : NetworkBehaviour
{
    public static playerAttack _instance;
    private Animator animator;
    private Rigidbody rb;

    private float coolingTimer = 0.6f;
    private float currentTime = 0.0f;

    public Transform detector;
    public LayerMask lm;

    public Transform top;
    public Transform down;

    public Transform leftFoot;
    public Transform rightFoot;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _instance = this;
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

        if (Input.GetMouseButtonDown(1) && !PlayerHealth._instance.isdead)
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

    public bool inAir()
    {
        //if((Physics.OverlapCapsule(down.position, top.position, 0.13f, lm).Length != 0))
        //{
        //    Debug.Log(Physics.OverlapCapsule(down.position, top.position, 0.13f, lm)[0].name);
        //}
        //return !(Physics.OverlapCapsule(down.position, top.position, 0.13f, lm).Length != 0);
        if(Physics.Raycast(detector.position, Vector3.down, 0.9f, lm) &&
            (Physics.Raycast(leftFoot.position, Vector3.down, 0.22f, lm) ||
            Physics.Raycast(rightFoot.position, Vector3.down, 0.22f, lm)))
        {
            return false;
        }
        return true;

        //return !Physics.Raycast(detector.position, Vector3.down, 0.9f, lm);
    }

    private void attack()
    {

        if (inAir())
        {
            Debug.Log("return--inair");
            return;
        }
        if (currentTime < coolingTimer)
            return;

        if (rb.velocity.magnitude > 0 &&Input.GetKey(KeyCode.LeftShift) && !animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            AudioManager._instance.Flying_kick();
            animator.Play("flying_kick");
            currentTime = -1.1f;
        }

        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick"))
        {
            if (gameManager._instance.isFinalStage && !animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing"))
            {
                AudioManager._instance.Punch();
                animator.Play("stabbing");
                currentTime = 0.0f;
            }
            if (!gameManager._instance.isFinalStage && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
            {

                AudioManager._instance.Punch();
                animator.Play("punch");
                currentTime = 0.0f;
            }
        }

    }
}

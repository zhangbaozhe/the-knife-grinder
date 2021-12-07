using System;
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

    public bool inAir()
    {
        return !Physics.Raycast(detector.position, Vector3.down, 0.11f, lm);
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
        
        if (rb.velocity.magnitude >=6 && !animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            AudioManager._instance.Attack();
            animator.Play("flying_kick");
            currentTime = -1.1f;
        }

        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick"))
        { 
            if(gameManager._instance.isFinalStage && !animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing"))
            {
                AudioManager._instance.Attack();
                animator.Play("stabbing");
                currentTime = 0.0f;
            }
            if (!gameManager._instance.isFinalStage && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
            {
                AudioManager._instance.Attack();
                animator.Play("punch");
                currentTime = 0.0f;
            }
        }
        
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
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
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
            attack();
        Delay();
        Debug.DrawLine(detector.position, new Vector3(detector.position.x, detector.position.y - 0.2f, detector.position.z), Color.red);
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
        RaycastHit[] hit;
        hit = Physics.RaycastAll(detector.position, Vector3.down, 0.2f, lm);
        //Debug.Log(hit.Length);
        if (hit.Length != 1)
        {
            Debug.Log("return--inair");
            
            //Debug.DrawLine(detector.position, new Vector3(detector.position.x, detector.position.y - Physics.RaycastAll(detector.position, Vector3.down, 0.2f, lm).Length, detector.position.z), Color.red);
            return;
        }
        if (currentTime < coolingTimer)
            return;
        
        if (rb.velocity.magnitude > 4 && !animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") && !animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            animator.Play("flying_kick");
            currentTime = -1.1f;
            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick"))
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "fist")
        {  
            animator.Play("get_hit");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    public static PlayerHealth _instance;
    private Animator animator;
    public int health = 100;
    public bool isdead = false;
    private Rigidbody rb;
    public GameObject myWeapon;
    public GameObject myFist;

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
    void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }
        if (health <= 0)
        {
            dead();
        }
    }
    private void dead()
    {
        animator.Play("dead");
        isdead = true;
        AudioManager._instance.Die();
        InGameUI._instance.ShowDeathInfo();
    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Ground")
            //Debug.Log(collision.collider.tag);
        if(collision.collider.tag == "darkness")
            {
                health = health - 100;
            }
        

        if(health <= 0)
        {
            dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground")
            //Debug.Log(other.tag);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("punch") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("get_hit"))
            {
                return;
            }
        if (other.tag == "fist")
        {
            myFist.GetComponent<SphereCollider>().enabled = false;

            if (playerAttack._instance.inAir())
            {
                Debug.Log("inair --hit");
                rb.AddExplosionForce(50.0f, other.transform.position, 1.0f);
            }
            else
            {
                Debug.Log("grounded --hit");
                rb.AddExplosionForce(150.0f, other.transform.position, 1.0f);
            }
            //Vector3 delta = (transform.position - collision.collider.transform.position).normalized;
            //Vector3 force = new Vector3(delta.x * 1000, delta.y * 500, delta.z * 1000);
            /*rb.position.Set(Mathf.Lerp(transform.position.x, transform.position.x + delta.x * 100, 0.5f),
                Mathf.Lerp(transform.position.y, transform.position.y + delta.y * 1000, 0.5f),
                Mathf.Lerp(transform.position.z, transform.position.z + delta.z * 1000, 0.5f)); */
            //Vector3 newPosition = transform.position + delta;
            //Debug.Log(transform.position.ToString()+ "--before");
            //if (playerAttack._instance.inAir())
            //   rb.AddForce(Vector3.up * 1000);
            //rb.AddForce(force);

            //Debug.Log(transform.position.ToString() + "--after");
            animator.Play("get_hit");
            AudioManager._instance.Hit();
            health = health - 8;
        }
        else if (other.tag == "foot")
        {
            if (playerAttack._instance.inAir())
                rb.AddExplosionForce(70.0f, other.transform.position, 1.0f);
            else
                rb.AddExplosionForce(200.0f, other.transform.position, 1.0f);

            animator.Play("get_hit");
            AudioManager._instance.Hit();
            health = health - 13;
        }
        else if (other.tag == "knife")
        {
            myWeapon.GetComponent<BoxCollider>().enabled = false;
            animator.Play("get_hit");
            AudioManager._instance.Hit();
            health = health - 32;
        }
        if (health <= 0)
        {
            dead();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "fist")
        {
            Debug.Log("Trigger Exit--fist re-active");
            myFist.GetComponent<SphereCollider>().enabled = true;
        }
        else if(other.tag == "knife")
        {
            Debug.Log("Trigger Exit--knife re-active");
            myWeapon.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

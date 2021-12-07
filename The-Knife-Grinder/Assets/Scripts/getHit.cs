using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getHit : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public int health = 100;
    public bool dead = false;
    // Start is called before the first frame update
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("punch 0") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick 0") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing 0") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("get_hit 0"))
        {
            return;
        }
        if (collision.collider.tag == "fist")
        {
            if (playerAttack._instance.inAir())
            {
                Debug.Log("inair --hit");
                rb.AddExplosionForce(1500.0f, collision.transform.position, 1.0f);
            }
            else
            {
                Debug.Log("grounded --hit");
                rb.AddExplosionForce(30000.0f, collision.transform.position, 1.0f);
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
            animator.Play("get_hit 0");
            AudioManager._instance.Hit();
            health = health - 8;
        }
        else if (collision.collider.tag == "foot")
        {
            if (playerAttack._instance.inAir())
                rb.AddExplosionForce(1.0f, collision.transform.position, 1.0f);
            else
                rb.AddExplosionForce(30000.0f, collision.transform.position, 1.0f);

            animator.Play("get_hit 0");
            AudioManager._instance.Hit();
            health = health - 13;
        }
        else if (collision.collider.tag == "knife")
        {
            animator.Play("get_hit 0");
            AudioManager._instance.Hit();
            health = health - 22;
        }
        if (health <= 0)
        {
            die();
        }
    }
    private void die()
    {
        animator.Play("dead 0");
        dead = true;
    }
}

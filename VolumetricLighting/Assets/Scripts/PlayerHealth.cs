using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
public class PlayerHealth : NetworkBehaviour
{
    public static PlayerHealth _instance;
    private Animator animator;
    private float win_time;
    private bool is_stop = false;
    public int health = 100;

    [SyncVar]
    public bool isdead = false;

    [SyncVar]
    public bool isWin = false;

    private Rigidbody rb;
    public GameObject myWeapon;
    public GameObject myFist;
    public GameObject myFoot;

    private double deathTime = 0;
    private static double realdeathTime = 0;
    // death related
    [Command]
    public void CmdSetupDeath(bool _dead)
    {
        isdead = _dead;
    }

    [Command]
    public void CmdIsWin()
    {
        RpcCheckIsWin(this.gameObject);
    }

    [ClientRpc]
    public void RpcCheckIsWin(GameObject player)
    { 
        if (player.GetComponent<PlayerHealth>().isdead)
        {
            isWin = false;
            return;
        }
        else
        {
            GameObject[] tempGameObjects = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < tempGameObjects.Length; i++)
            {
                // Debug.Log(tempGameObjects[i].name);
                if (tempGameObjects[i] == player)
                {
                    continue;
                }
                if (!tempGameObjects[i].GetComponent<PlayerHealth>().isdead)
                {
                    isWin = false;
                    return;
                }
            }
            isWin = true;
        }
    }



    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        if (!isLocalPlayer) { return; }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(deathTime);
        if (isdead && (Time.time - deathTime) >= 4f)
        {
            
            
            if (gameObject.activeSelf)
            {
                
                gameObject.SetActive(false);
                PlayerCameraManager._instance.Disactive();
                gameManager._instance.ActiveDeadCam();
            }
            //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
            //    {
            //        myFist.SetActive(true);
            //    }
            //    else if (animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick"))
            //    {
            //        myFoot.SetActive(true);
            //    }
            //    else if (animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing"))
            //    {
            //        myWeapon.SetActive(true);
            //    }
            //    else
            //    {
            //        myFist.SetActive(false);
            //        myFist.SetActive(false);
            //        myFist.SetActive(false);
            //    }
        }
    }
    void FixedUpdate()
        {
            if (!isLocalPlayer) { return; }
            if (health <= 0)
            {
                if(!isdead)
                    dead();
            }

            // wait for all players come in
            // TODO: write some fucntions to detect all palyers comming in
            if (Counter._instance.times <= 225 - 45.0f)
            {
                CmdIsWin();
                if (isWin && !is_stop)
                {
                win_time = Time.time + 5f;
                is_stop = true;
                }
                if(isWin)
                {
                    InGameUI._instance.ShowWinInfo();
                    Debug.Log("WINWINWIN");
                // I quit. Mirror is so hard ...

                if(Time.time >= win_time)
                {
                    SceneManager.LoadScene("Area Light");
                    //Application.Quit();
                }
                
                }
            }
        }
    
    private void dead()
    {
        if (!isLocalPlayer) { return;  }
        animator.Play("dead");
        // isdead = true;
        CmdSetupDeath(true);
        AudioManager._instance.Die();
        InGameUI._instance.ShowDeathInfo();
        deathTime = Time.time;
        realdeathTime = deathTime;
    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Ground")
            //Debug.Log(collision.collider.tag);
        if(collision.collider.tag == "darkness")
            {
                health = health - 100;
            }
        


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground")
            //Debug.Log(other.tag);
        if (
        //    animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") ||
        //    animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("get_hit"))
        {
            return;
        }



        if (other.tag == "fist")
            {
                //myFist.GetComponent<SphereCollider>().enabled = false;

                if (playerAttack._instance.inAir())
                {
                    Debug.Log("inair --hit");
                    rb.AddExplosionForce(1500.0f, other.transform.position, 1.0f);
                }
                else
                {
                    Debug.Log("grounded --hit");
                    rb.AddExplosionForce(5000.0f, other.transform.position, 1.0f);
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
                    rb.AddExplosionForce(3000.0f, other.transform.position, 1.0f);
                else
                    rb.AddExplosionForce(8000.0f, other.transform.position, 1.0f);

                animator.Play("get_hit");
                AudioManager._instance.Hit();
                health = health - 13;
            }
            else if (other.tag == "knife")
            {
                //myWeapon.GetComponent<BoxCollider>().enabled = false;
                animator.Play("get_hit");
                AudioManager._instance.Hit();
                health = health - 32;
            }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "fist")
        {
            Debug.Log("Trigger Exit--fist re-active");
            //myFist.GetComponent<SphereCollider>().enabled = true;
        }
        else if(other.tag == "knife")
        {
            Debug.Log("Trigger Exit--knife re-active");
            //myWeapon.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

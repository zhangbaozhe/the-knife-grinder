using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerHealth : NetworkBehaviour
{
    public static PlayerHealth _instance;
    private Animator animator;
    public int health = 100;

    [SyncVar]
    public uint playerID;

    [SyncVar]
    public bool isdead = false;

    [SyncVar]
    public bool isWin = false;

    // sync attacking stuff
    [SyncVar]
    public bool isSetFist = true;

    [SyncVar]
    public bool isSetFoot = true;

    [SyncVar]
    public bool isSetWeapon = true;

    private Rigidbody rb;
    public GameObject myWeapon;
    public GameObject myFist;
    public GameObject myFoot;

    /*[Command]
    public void CmdSetFistActive(bool _isSetFist, uint targetID)
    {
        isSetFist = _isSetFist;
        RpcSetFistActive(_isSetFist, targetID);
    }

    [Command]
    public void CmdSetFootActive(bool _isSetFoot, uint targetID)
    {
        isSetFoot = _isSetFoot;
        RpcSetFootActive(_isSetFoot, targetID);
    }

    [Command]
    public void CmdSetWeaponActive(bool _isSetWeapon, uint targetID)
    {
        isSetWeapon = _isSetWeapon;
        RpcSetWeaponActive(_isSetWeapon, targetID);
    }

    [ClientRpc]
    public void RpcSetFistActive(bool _isSetFist, uint targetID)
    {
        GameObject[] tempGameObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tempGameObjects.Length; i++)
        {
            // Debug.Log(tempGameObjects[i].name);
            if (tempGameObjects[i].GetComponent<NetworkIdentity>().netId == targetID)
            {
                if (tempGameObjects[i].transform.Find("fist") == null) { return; }
                tempGameObjects[i].transform.Find("fist").gameObject.SetActive(_isSetFist);
                return;
            }
        }
    }

    [ClientRpc]
    public void RpcSetFootActive(bool _isSetFoot, uint targetID)
    {
        GameObject[] tempGameObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tempGameObjects.Length; i++)
        {
            // Debug.Log(tempGameObjects[i].name);
            if (tempGameObjects[i].GetComponent<NetworkIdentity>().netId == targetID)
            {
                if (tempGameObjects[i].transform.Find("foot") == null) { return; }
                tempGameObjects[i].transform.Find("foot").gameObject.SetActive(_isSetFoot);
                return;
            }
        }
    }

    [ClientRpc]
    public void RpcSetWeaponActive(bool _isSetWeapon, uint targetID)
    {
        GameObject[] tempGameObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < tempGameObjects.Length; i++)
        {
            // Debug.Log(tempGameObjects[i].name);
            if (tempGameObjects[i].GetComponent<NetworkIdentity>().netId == targetID)
            {
                if (tempGameObjects[i].transform.Find("Tanton") == null) { return; }
                tempGameObjects[i].transform.Find("Tanton").gameObject.SetActive(_isSetWeapon);
                return;
            }
        }
    }*/

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
        // network ID
        //playerID = this.gameObject.GetComponent<NetworkIdentity>().netId;
    }

    // Update is called once per frame
    private void Update()
    {
        /*if (!isLocalPlayer) { return; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("punch"))
        {
            myFist.SetActive(true);
            //CmdSetFistActive(true, playerID);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick"))
        {
            myFoot.SetActive(true);
            //CmdSetFootActive(true, playerID);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing"))
        {
            myWeapon.SetActive(true);
            //CmdSetWeaponActive(true, playerID);
        }
        else
        {
            myFist.SetActive(false);
            myWeapon.SetActive(false);
            myFoot.SetActive(false);
            //CmdSetFistActive(false, playerID);
            //CmdSetFootActive(false, playerID);
            //CmdSetWeaponActive(false, playerID);
        }*/

    }
    void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }
        if (health <= 0)
        {
            dead();
        }

        // wait for all players come in
        // TODO: write some fucntions to detect all palyers comming in
        if (Counter._instance.times <= 225 - 45.0f)
        {
            //CmdIsWin();
            if (isWin)
            {
                InGameUI._instance.ShowWinInfo();
                Debug.Log("WINWINWIN");
            }
        }
    }
    private void dead()
    {
        if (!isLocalPlayer) { return; }
        animator.Play("dead");
        // isdead = true;
        CmdSetupDeath(true);
        AudioManager._instance.Die();
        InGameUI._instance.ShowDeathInfo();

        gameObject.SetActive(false);
        PlayerCameraManager._instance.Disactive();
        gameManager._instance.ActiveDeadCam();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Ground")
            //Debug.Log(collision.collider.tag);
            if (collision.collider.tag == "darkness")
            {
                health = health - 100;
            }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground")
            //Debug.Log(other.tag);
            /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("punch") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("flying_kick") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing") ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("get_hit"))
            {
                return;
            }*/



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
                    rb.AddExplosionForce(700.0f, other.transform.position, 1.0f);
                else
                    rb.AddExplosionForce(2000.0f, other.transform.position, 1.0f);

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
        if (other.tag == "fist")
        {
            Debug.Log("Trigger Exit--fist re-active");
            //myFist.GetComponent<SphereCollider>().enabled = true;
        }
        else if (other.tag == "knife")
        {
            Debug.Log("Trigger Exit--knife re-active");
            //myWeapon.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

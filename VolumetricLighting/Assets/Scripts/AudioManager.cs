using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager  _instance;
    public AudioClip run;
    public AudioClip punch;
    public AudioClip flying_kick;
    public AudioSource getHit;
    public AudioSource move;
    public AudioSource attack_human;
    public AudioSource getHit_human;
    public AudioSource jump_human;
    public AudioSource inair;
    public AudioSource die;
    private int deathcount = 0;
    //private Rigidbody rb;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
        runAudio();
        jumpAudio();
    }

    void jumpAudio()
    {
        if(Input.GetKey(KeyCode.Space) && !playerAttack._instance.inAir())
        {
            jump_human.Play();
        }
        
    }
    void runAudio()
    {
        float x, y;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if(!playerAttack._instance.inAir() && (x!=0||y!=0))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                //Debug.Log("sprint");
                move.pitch = 1.2f;
            }
            else
            {
                move.pitch = 1f;
            }
            if (move.isPlaying)
                return;
            
            move.clip = run;
            move.volume = 1;
            move.Play();
            //Debug.Log(rb.velocity.magnitude);
        }
        else
        {
            //Debug.Log("stop --" + playerAttack._instance.inAir().ToString());
            move.volume = Mathf.Lerp(1, 0, 0.8f);
            move.Stop();
        }
    }
    public void Hit()
    {
        getHit.Play();
        getHit_human.Play();
    }
    public void Punch()
    {
        attack_human.clip = punch;
        attack_human.Play();
    }
    public void Flying_kick()
    {
        attack_human.clip = flying_kick;
        attack_human.Play();
    }
    public void Die()
    {
        deathcount++;
        if (deathcount > 1)
            return;
        //Debug.Log(die.isPlaying);
        if (!die.isPlaying)
        {
            die.Play();
            //Debug.Log(die.isPlaying);
        }
        
    }
}

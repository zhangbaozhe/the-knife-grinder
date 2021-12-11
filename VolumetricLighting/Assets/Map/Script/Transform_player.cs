using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_player : MonoBehaviour
{
    public Rigidbody player_rb;
    public Transform player;
    private bool hasTransformed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Counter._instance.times <= 50 && !hasTransformed)
        {
            player_rb.velocity = new Vector3(0f, 0f, 0f);
            player.position = new Vector3(0f, 130f, 330f);
            hasTransformed = true;
        }
    }
}

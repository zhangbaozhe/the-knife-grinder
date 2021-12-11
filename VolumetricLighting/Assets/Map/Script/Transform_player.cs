using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transform_player : MonoBehaviour
{
    public Transform player;
    private bool hasTransformed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Counter._instance.times <= 215 && !hasTransformed)
        {
            player.position = new Vector3(0f, 130f, 330f);
            hasTransformed = true;
        }
    }
}

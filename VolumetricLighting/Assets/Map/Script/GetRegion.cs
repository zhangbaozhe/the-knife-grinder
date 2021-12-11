using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRegion : MonoBehaviour
{
    public float region_num;
    public Transform tf;

    private float x;
    private float z;
    private bool is_color;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Counter._instance.times >= 180 && !is_color)
        {
            x = tf.position.x;
            z = tf.position.z;

            if(-100 < x && -90 > x && 25 < z && 35 > z)
            {
                region_num = 1f;
            }else if(-5 < x && 5 > x && 95 < z && 105 > z)
            {
                region_num = 2f;
            }else if (90 < x && 100 > x && 25 < z && 35 > z)
            {
                region_num = 3f;
            }else if (55 < x && 65 > x && -85 < z && -75 > z)
            {
                region_num = 4f;
            }
            else
            {
                region_num = 5f;
            }

            is_color = true;
        }
    }
}

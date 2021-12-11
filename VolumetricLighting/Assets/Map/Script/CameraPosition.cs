using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform camera_position;
    private bool hasTransformed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Counter._instance.times <= 50 && !hasTransformed)
        {
            camera_position.position = new Vector3(0f, 129f, 303f);
            hasTransformed = true;
            camera_position.rotation = Quaternion.Euler(Vector3.left * 11);
        }
    }
}

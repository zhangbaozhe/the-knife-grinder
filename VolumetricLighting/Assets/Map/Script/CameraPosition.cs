using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Transform camera_position;
    public bool isEnding = true;
    private bool hasTransformed = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnding && !hasTransformed)
        {
            camera_position.position = new Vector3(0f, 129f, 303f);
            hasTransformed = true;
            camera_position.Rotate(Vector3.left * 11) ;

        }
    }
}

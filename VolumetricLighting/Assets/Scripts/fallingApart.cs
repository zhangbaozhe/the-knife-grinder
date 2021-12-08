using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingApart : MonoBehaviour
{
    public bool isFalling = false;
    public bool isShaking = false;
    public Transform tr;

    private int i = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFalling)
        {
            tr.gameObject.SetActive(false); ;
        }

        if (isShaking)
        {
            tr.position = Vector3.Lerp(tr.position, tr.position + new Vector3(0, i * 3), Time.deltaTime * 5);
            i = -i;
        }
    }
}

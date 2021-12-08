using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debuger : MonoBehaviour
{
    // Start is called before the first frame update
    Collider collider;
    void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("11111");
        //Debug.Log(collider.gameObject.name);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
    }
}

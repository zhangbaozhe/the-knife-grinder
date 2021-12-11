using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRim : MonoBehaviour
{

    public Material source;
    private Material myMat;
    
    void Start()
    {
        
        //smr = GetComponent<SkinnedMeshRenderer>();
        myMat = new Material(source);
        Debug.Log(myMat.shader.name);
        myMat.SetColor("_AtmoColor", Color.blue);
        Debug.Log(myMat.shader.FindPropertyIndex("_AtmoColor"));
            
        Material[] Mat = new Material[2] { myMat, myMat };
        transform.GetComponent<Renderer>().materials = Mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

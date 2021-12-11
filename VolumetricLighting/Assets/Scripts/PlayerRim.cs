using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRim : MonoBehaviour
{

    public Material source;
    private Material myMat;
    private bool colorChanged = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Counter._instance.times >= 180f && !colorChanged)
        {
            myMat = new Material(source);
            Debug.Log(GetRegion._instance.region_num);
            if (GetRegion._instance.region_num == 1)
                myMat.SetColor("_AtmoColor", new Color32(26, 115, 22, 255));
            else if (GetRegion._instance.region_num == 2)
                myMat.SetColor("_AtmoColor", new Color32(48, 105, 150, 255));
            else if (GetRegion._instance.region_num == 4)
                myMat.SetColor("_AtmoColor", new Color32(153, 154, 74, 255));
            else if (GetRegion._instance.region_num == 3)
                myMat.SetColor("_AtmoColor", new Color32(48, 105, 150, 255));
            else if (GetRegion._instance.region_num == 5)
                myMat.SetColor("_AtmoColor", new Color32(255, 255, 255, 255));
            //Debug.Log(myMat.shader.FindPropertyIndex("_AtmoColor"));

            Material[] Mat = new Material[2] { myMat, myMat };
            transform.GetComponent<Renderer>().materials = Mat;
            colorChanged = true;
        }
    }
}

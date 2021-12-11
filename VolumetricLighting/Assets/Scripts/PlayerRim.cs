﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerRim: NetworkBehaviour
{

    public Material source;
    // private Material myMat;
    private bool colorChanged = false;
    public GameObject childBody;

    // sync color
    [SyncVar(hook = nameof(OnRimColorChanged))]
    public Color32 rimColor = new Color32(48, 105, 150, 255);

    void OnRimColorChanged(Color32 _old, Color32 _new)
    {
        Material myMat = new Material(source);
        myMat.SetColor("_AtmoColor", _new);
        Material[] Mat = new Material[2] { myMat, myMat };
        childBody.GetComponent<Renderer>().materials = Mat;
        // childBody.GetComponent<Renderer>().materials[0].color = _new;
        // childBody.GetComponent<Renderer>().materials[1].color = _new;

    }

    [Command]
    public void CmdSetRimColor(Color32 _color)
    {
        rimColor = _color;
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }
        if(Counter._instance.times >= 180f && !colorChanged)
        {
            // myMat = new Material(source);
            Debug.Log(GetRegion._instance.region_num);
            if (GetRegion._instance.region_num == 1)
                CmdSetRimColor(new Color32(26, 115, 22, 255));
            else if (GetRegion._instance.region_num == 2)
                CmdSetRimColor(new Color32(48, 105, 150, 255));
            else if (GetRegion._instance.region_num == 4)
                CmdSetRimColor(new Color32(153, 154, 74, 255));
            else if (GetRegion._instance.region_num == 3)
                CmdSetRimColor(new Color32(48, 105, 150, 255));
            else if (GetRegion._instance.region_num == 5)
                CmdSetRimColor(new Color32(255, 255, 255, 255));
            //Debug.Log(myMat.shader.FindPropertyIndex("_AtmoColor"));

            // Material[] Mat = new Material[2] { myMat, myMat };
            // transform.GetComponent<Renderer>().materials = Mat;
            colorChanged = true;
        }
    }
}

using System.Collections;
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
    public Color rimColor;

    void OnRimColorChanged(Color _old, Color _new)
    {
        Material myMat = new Material(source);
        myMat.SetColor("_AtmoColor", _new);
        Material[] Mat = new Material[2] { myMat, myMat };
        childBody.GetComponent<Renderer>().materials = Mat;
        // childBody.GetComponent<Renderer>().materials[0].SetColor("EmissionColor", _new);
        // childBody.GetComponent<Renderer>().materials[1].SetColor("EmissionColor", _new);

    }

    [Command]
    public void CmdSetRimColor(Color _color)
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
            // Debug.Log(GetRegion._instance.region_num);
            if (GetRegion._instance.region_num == 1)
            {
                CmdSetRimColor(Color.green);
            }
            if (GetRegion._instance.region_num == 2)
            {
                CmdSetRimColor(Color.blue);
                // Debug.Log("ENTERING 2");
            }
            if (GetRegion._instance.region_num == 4)
            {
                CmdSetRimColor(Color.yellow);
            }
            if (GetRegion._instance.region_num == 3)
            {
                CmdSetRimColor(Color.red);
            }
            if (GetRegion._instance.region_num == 5)
            {
                CmdSetRimColor(Color.white);
            }
            //Debug.Log(myMat.shader.FindPropertyIndex("_AtmoColor"));

            // Material[] Mat = new Material[2] { myMat, myMat };
            // transform.GetComponent<Renderer>().materials = Mat;
            colorChanged = true;
        }
    }
}

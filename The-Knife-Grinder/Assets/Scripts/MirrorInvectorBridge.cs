using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Invector.vCharacterController;

public class MirrorInvectorBridge : NetworkBehaviour
{
    public GameObject playerCamera;
    public GameObject playerCam;

    private vThirdPersonInput vTPI;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<vThirdPersonInput>().enabled = false; 
        }
        else
        {
            playerCam = Instantiate(playerCamera);
            playerCam.name = this.gameObject.name + "camera";
            vTPI = GetComponent<vThirdPersonInput>();
            InitializeTpCamera();
            
        }
    }

    public virtual void InitializeTpCamera()
    {


        var tpCamera = playerCam.GetComponent<vThirdPersonCamera>();



        if (tpCamera)
        {
            tpCamera.SetMainTarget(this.transform);
            tpCamera.Init();
        }
        vTPI.tpCamera = tpCamera;
    }
}

using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Invector.vCharacterController;

public class GrapplingGun : NetworkBehaviour {

    public Transform gg;
    private Vector3 grapplePoint; 
    public LayerMask whatIsGrappleable;
    

    public Transform gunTip,player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private Transform thirdCamera;
    private Vector3 currentGrapplePosition;


    private LineRenderer lr;


    // Glow material
    public GameObject childBody;
    private Material playerMaterialClone;
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    void OnColorChanged(Color _Old, Color _New) {
        //Debug.Log("OnColorChanged");
        childBody.GetComponent<SkinnedMeshRenderer>().materials[0].color = _New;
        childBody.GetComponent<SkinnedMeshRenderer>().materials[1].color = _New;
    }

    [Command]
    public void CmdSetupPlayer(Color _color)
    {
        playerColor = _color; 
    }
        

    //public GameObject aim;
    //private Image aim_source;

    private RaycastHit _hit;
    //private GameObject aim;
    //private Image aim_source;

    void Awake() {
        // TODO: Mirrir related bugs
        // lr = GetComponent<LineRenderer>();
        // FIXME: line bug
        lr = gg.gameObject.GetComponent<LineRenderer>(); // TODO: the line is not working in Mirrio
        lr.positionCount = 0;
        if (isLocalPlayer)
        {
            while (GetComponent<vThirdPersonInput>().tpCamera == null)
            {
                ;
            }
            thirdCamera = GetComponent<vThirdPersonInput>().tpCamera.transform;
        }
    }
    private void Start()
    {

        //aim_source = FindObjectOfType<Image>();
    }
    void Update() {
        if (isLocalPlayer)
        {
            


            if (Input.GetMouseButtonDown(0))
            {

                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
            }
        }
    }

    //Called after Update
    void LateUpdate() {
        if (!isLocalPlayer) { return; }
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// 1# ????????
    /// 2# ????
    /// 
    /// </summary>
    void StartGrapple() {

        thirdCamera = GetComponent<vThirdPersonInput>().tpCamera.transform;
        if (joint)
        {
            Destroy(joint);
        }
        RaycastHit hit;
        // ?????????????????? ????????????????????camera
        if (Physics.Raycast(thirdCamera.position, thirdCamera.forward, out hit, maxDistance, whatIsGrappleable)) {
            //aim_source.color = new Color32(255, 255, 255, 255);
            //AudioManager._instance.inAir();
            grapplePoint = hit.point; //??????
            
            joint = player.gameObject.AddComponent<SpringJoint>(); //??????????springjoint
            joint.autoConfigureConnectedAnchor = false; //????????????????????
            
            joint.connectedAnchor = grapplePoint; //????????????

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint); //??????????????????
            //Debug.Log(distanceFromPoint);

            //The distance grapple will try to keep from grapple point. ????????????
            joint.maxDistance = distanceFromPoint * 0.5f; 
            joint.minDistance = distanceFromPoint * 0.3f;

            //Adjust these values to fit your game.
            joint.spring = 8f; //????????,????????
            joint.damper = 3f;  //????????????????????????????
            joint.massScale = 3f; //????????????????????????????
            

            lr.positionCount = 2; //lr??????????????????????????????????????
            currentGrapplePosition = gunTip.position; //????????gunTip??????????????????????
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        //aim_source.color = new Color32(255, 255, 255, 160);
        //AudioManager._instance.inAir();
        lr.positionCount = 0;
        Destroy(joint);
    }


    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint)
        {
            CmdSetupPlayer(Color.white);
            return; 
        }

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        //????????
        lr.SetPosition(0, gunTip.position);

        lr.SetPosition(1, currentGrapplePosition);

        // Glow if has rope 
        CmdSetupPlayer(Color.yellow);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}

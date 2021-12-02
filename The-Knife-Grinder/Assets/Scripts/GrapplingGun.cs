using UnityEngine;
using Mirror;
using Invector.vCharacterController;

public class GrapplingGun : NetworkBehaviour {

    public Transform gg;
    private Vector3 grapplePoint; 
    public LayerMask whatIsGrappleable; 

    public Transform gunTip,player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private Transform camera;
    private LineRenderer lr;

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
            camera = GetComponent<vThirdPersonInput>().tpCamera.transform;
        }
    }

    void Update() {
        if (isLocalPlayer)
        {
            
            camera = GetComponent<vThirdPersonInput>().tpCamera.transform;
            Debug.Log(camera.ToString());

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
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// 1# ????????
    /// 2# ????
    /// 
    /// </summary>
    void StartGrapple() {
        if (joint)
        {
            Destroy(joint);
        }
        RaycastHit hit;
        // ?????????????????? ????????????????????camera
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point; //??????
            
            joint = player.gameObject.AddComponent<SpringJoint>(); //??????????springjoint
            joint.autoConfigureConnectedAnchor = false; //????????????????????
            
            joint.connectedAnchor = grapplePoint; //????????????

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint); //??????????????????

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
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        //????????
        lr.SetPosition(0, gunTip.position);

        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}

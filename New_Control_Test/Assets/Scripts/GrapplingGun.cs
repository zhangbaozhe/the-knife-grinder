using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Vector3 grapplePoint; 
    public LayerMask whatIsGrappleable; 

    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0)) {
            StopGrapple();
        }
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        // 检测是否能碰撞上， 检测碰撞的起始点来自camera
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point; //撞击点
            joint = player.gameObject.AddComponent<SpringJoint>(); //创建对应的springjoint
            joint.autoConfigureConnectedAnchor = false; //禁止自动连接锚点位置
            joint.connectedAnchor = grapplePoint; //加一个连接点

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint); //计算两者之间的距离

            //The distance grapple will try to keep from grapple point. 这里需要大改
            joint.maxDistance = distanceFromPoint * 0.8f; 
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f; //弹簧强度
            joint.damper = 7f;  //弹簧作为活性状态下的压缩程度
            joint.massScale = 4.5f; //解决彼此速度的问题，勾中玩家

            lr.positionCount = 2; //lr，中间的断点数目，可以用于生成抛物线等
            currentGrapplePosition = gunTip.position; //记录当前gunTip的位置，方便后续的计算
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
        
        //射出位置
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

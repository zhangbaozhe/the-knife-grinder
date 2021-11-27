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
    /// 1# ����λ��
    /// 2# ����
    /// 
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        // ����Ƿ�����ײ�ϣ� �����ײ����ʼ������camera
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point; //ײ����

            //player.position = Vector3.MoveTowards(player.position, grapplePoint, 10f);
            //Rigidbody rb = player.gameObject.GetComponent<Rigidbody>();
            Debug.Log(grapplePoint);
            //Debug.Log(player.position);
            //Vector3 moveTo = grapplePoint - player.position;
            //rb.AddForce(moveTo * Time.deltaTime * 10000);
            //Debug.Log(Vector3.MoveTowards(grapplePoint, player.position, 1f));
            
            joint = player.gameObject.AddComponent<SpringJoint>(); //������Ӧ��springjoint
            joint.autoConfigureConnectedAnchor = false; //��ֹ�Զ�����ê��λ��
            
            joint.connectedAnchor = grapplePoint; //��һ�����ӵ�

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint); //��������֮��ľ���

            //The distance grapple will try to keep from grapple point. ������Ҫ���
            joint.maxDistance = distanceFromPoint * 0.7f; 
            joint.minDistance = distanceFromPoint * 0.5f;

            //Adjust these values to fit your game.
            joint.spring = 10f; //����ǿ��,�Ƚ���Ҫ
            joint.damper = 0f;  //������Ϊ����״̬�µ�ѹ���̶�
            joint.massScale = 4.5f; //����˴��ٶȵ����⣬�������
            

            lr.positionCount = 2; //lr���м�Ķϵ���Ŀ�������������������ߵ�
            currentGrapplePosition = gunTip.position; //��¼��ǰgunTip��λ�ã���������ļ���
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
        
        //���λ��
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

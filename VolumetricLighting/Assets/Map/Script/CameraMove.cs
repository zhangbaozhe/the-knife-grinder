using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    
    public Transform playerCam;
    public static CameraMove Instance = null;

    private Vector3 dirVector3;
    private Vector3 rotaVector3;
    private float paramater = 0.1f;
    public AudioSource bgm;
    public AudioClip battleTheme;
    //Rotation and look
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    private bool startFinalStage = false;
    private bool BGMSwitched = false;
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rotaVector3 = transform.localEulerAngles;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Counter._instance.times <=60.0f)
        {
            if (!startFinalStage)
            {
                LowVolume();
            }
        }

        if(Counter._instance.times <= 55.0f)
        {
            if (!BGMSwitched)
            {
                SwitchBGM();
            }
        }
    }
    void FixedUpdate()
    {
            Look();

        //移动
        dirVector3 = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.z = 3;
            else dirVector3.z = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.z = -3;
            else dirVector3.z = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = -3;
            else dirVector3.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.x = 3;
            else dirVector3.x = 1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = -3;
            else dirVector3.y = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.LeftShift)) dirVector3.y = 3;
            else dirVector3.y = 1;
        }
        transform.Translate(dirVector3 * paramater, Space.Self);
        //限制摄像机范围
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        //playerCam.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    private void LowVolume()
    {
        bgm.volume = Mathf.Lerp(bgm.volume, 0f, 0.5f);
        startFinalStage = true;
    }
    private void SwitchBGM()
    {
        bgm.clip = battleTheme;
        bgm.volume = Mathf.Lerp(bgm.volume, 1f, 0.5f);
        BGMSwitched = true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager _instance;

    //private float TotalTime = 360.0f;
    //private float finalStageTigger = 75.0f;
    public bool isFinalStage = false;
    public GameObject deadCam;
    private bool knifeDrawed = false;
    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Counter._instance.times <= 61.0f)
        {
            if(!isFinalStage)
                isFinalStage = true;
            /*if (!knifeDrawed)
            {
                playerAttack._instance.drawKnife();
                knifeDrawed = true;
            }*/
        }
    }

    public void ActiveDeadCam()
    {
        deadCam.SetActive(true);
    }
}

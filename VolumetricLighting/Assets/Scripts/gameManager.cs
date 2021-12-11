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
            isFinalStage = true;
            playerAttack._instance.drawKnife();
        }
    }

    
}

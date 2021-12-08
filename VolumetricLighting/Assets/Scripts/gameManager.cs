using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager _instance;

    private float TotalTime = 360.0f;
    private float finalStageTigger = 75.0f;
    public bool isFinalStage = false;
    public GameObject text;
    private Text timeInfo;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        timeInfo = text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TotalTime > 0)
        {
            TotalTime -= Time.deltaTime;
        }
        timeInfo.text = ((int)TotalTime).ToString();
        if(!isFinalStage && TotalTime <= finalStageTigger)
        {
            isFinalStage = true;
        }
    }

    
}

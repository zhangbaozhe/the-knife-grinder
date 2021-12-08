using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public GameObject Light;
    public GameObject timeManager;
    public float interval = 1f;
    public float startTime = 225f;
    public float endTime = 195f;

    private bool isFlash = false;
    private float _time; 
    float nextFlashTime = 225;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time = timeManager.GetComponent<Counter>().times;
        if (_time >= endTime && _time <= startTime)
        {
            if (isFlash && _time < nextFlashTime)
            {
                Light.GetComponent<TubeLight>().m_Color = Color.red;
                isFlash = false;
                nextFlashTime = _time - interval;
            }
            else if (_time < nextFlashTime)
            {
                Light.GetComponent<TubeLight>().m_Color = Color.white;
                isFlash = true;
                nextFlashTime = _time - interval;
            }
        }
        if(_time < endTime)
        {
            Light.GetComponent<TubeLight>().m_Color = Color.red;
        }

    }
}

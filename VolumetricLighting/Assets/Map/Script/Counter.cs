using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Counter : NetworkBehaviour
{
	public static Counter _instance;
	private Text TimeText;
	public double times = 225;
	[SyncVar]
	private double s;//定义一个秒

	public GameObject[] firstLevel = new GameObject[5];
	public GameObject[] secondLevel = new GameObject[16];

    private void Awake()
    {
		_instance = this;
    }
    void Start()
	{
		TimeText = GameObject.Find("TimeText").GetComponent<Text>();
	}

	void FixedUpdate()
	{
		//计时器完成倒计时的功能
		times = 225 - NetworkTime.time;
		s = (int)times % 225; //小数转整数 
		TimeText.text = s.ToString();
		//Debug.Log(times);

		// Baozhe: first level shaking and falling
		if (s >= 170 && s < 180)
        {
			if (s == 170)
            {
				for (int i = 0; i < 5; i++)
                {
					firstLevel[i].GetComponent<fallingApart>().isFalling = true;
                }
			}
			else
            {
				for (int i = 0; i < 5; i++)
                {
					firstLevel[i].GetComponent<fallingApart>().isShaking = true;
                }
            }

        }

		// Baozhe: second level shaking and falling
		if (s >= 110 && s < 120)
        {
			if (s == 110)
            {
				for (int i = 0; i < 16; i++)
                {
					secondLevel[i].GetComponent<fallingApart>().isFalling = true;
                }
            }
			else
            {
				for (int i = 0; i < 16; i++)
                {
					secondLevel[i].GetComponent<fallingApart>().isShaking = true;
                }
            }
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Counter : NetworkBehaviour
{
	private Text TimeText;
	public double times = NetworkTime.time + 225;
	[SyncVar]
	private double s;//定义一个秒

	public GameObject[] firstLevel = new GameObject[5];
	public GameObject[] secondLevel = new GameObject[16];

	void Start()
	{
		TimeText = GameObject.Find("TimeText").GetComponent<Text>();
	}

	void FixedUpdate()
	{
		//计时器完成倒计时的功能
		times -= Time.deltaTime;
		s = (int)times % 225; //小数转整数 
		TimeText.text = s.ToString();

		// Baozhe: first level shaking and falling
		if (s >= 155 && s < 165)
        {
			if (s == 155)
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
		if (s >= 95 && s < 105)
        {
			if (s == 95)
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

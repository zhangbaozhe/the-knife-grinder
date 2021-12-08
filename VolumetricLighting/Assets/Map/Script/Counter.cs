using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
	private Text TimeText;
	public float times = 225;
	private int s;//定义一个秒

	void Start()
	{
		TimeText = GameObject.Find("TimeText").GetComponent<Text>();
	}

	void Update()
	{
		//计时器完成倒计时的功能
		times -= Time.deltaTime;
		s = (int)times % 225; //小数转整数 
		TimeText.text = s.ToString();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimManager : MonoBehaviour
{
    public static AimManager _instance;
    public Sprite hooked;
    public Sprite unhooked;
    private Image aim;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        aim = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    public void NoTarget()
    {
        aim.sprite = unhooked;
        aim.color = new Color32(255, 255, 255, 160);
    }

    public void HasTarget()
    {
        aim.sprite = unhooked;
        aim.color = new Color32(255, 255, 255, 255);
    }

    public void Hook()
    {
        aim.sprite = hooked;
        aim.color = new Color32(255, 255, 255, 255);
    }
}

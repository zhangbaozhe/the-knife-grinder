using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip battleTheme;
    public static PlayerCameraManager _instance;
    private bool startFinalStage = false;
    private bool BGMSwitched = false;
    private void Awake()
    {
        _instance = this;
    }
    private void Update()
    {
        if (Counter._instance.times <= 60.0f)
        {
            if (!startFinalStage)
            {
                LowVolume();
            }
        }

        if (Counter._instance.times <= 55.0f)
        {
            if (!BGMSwitched)
            {
                SwitchBGM();
            }
        }
    }
    public void Disactive()
    {
        gameObject.SetActive(false);
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

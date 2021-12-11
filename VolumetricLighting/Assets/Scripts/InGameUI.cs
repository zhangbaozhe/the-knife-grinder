﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public static InGameUI _instance;
    public GameObject InGamePanel;
    public GameObject DeathInfo;

    private void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            onEsePressed();
        }
    }
    public void onEsePressed()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        InGamePanel.SetActive(!InGamePanel.activeSelf);
    }
    public void onQuitClick()
    {
        
        SceneManager.UnloadSceneAsync("Map");
        SceneManager.LoadScene("Area Light");
    }
    public void onLeaveGameClick()
    {
        Application.Quit();
    }
    public void ShowDeathInfo()
    {
        if(DeathInfo.activeSelf == false)
            DeathInfo.SetActive(true);
    }


}
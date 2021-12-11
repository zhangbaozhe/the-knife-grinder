using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public static InGameUI _instance;
    public GameObject InGamePanel;
    public GameObject DeathInfo;
    public GameObject WinInfo;
    public GameObject aim;

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
        if (DeathInfo.activeSelf)
        {
            DeathInfo.SetActive(false);
        }
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
        aim.SetActive(false);
    }
    public void ShowWinInfo()
    {
        if (WinInfo.activeSelf == false)
            WinInfo.SetActive(true);
        
    }

}

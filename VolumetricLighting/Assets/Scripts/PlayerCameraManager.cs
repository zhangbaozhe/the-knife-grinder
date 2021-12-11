using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    public static PlayerCameraManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void Disactive()
    {
        gameObject.SetActive(false);
    }

}

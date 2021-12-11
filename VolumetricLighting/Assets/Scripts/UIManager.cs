using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class UIManager : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public Button Play;
    public Button Host;
    public Button Client;
    public InputField ip;
    private NetworkManager manager;

    // Start is called before the first frame update
    private void Awake()
    {
        manager = GetComponent<NetworkManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayClick()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    public void onHostClick()
    {
        manager.StartHost();
    }
    public void onClientClick()
    {
        Debug.Log("Start Client"+ ip.text);
        
        manager.networkAddress = ip.text;
        manager.StartClient();
        
    }

    public void onButtonQuit2()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void onButtonQuit1()
    {
        Application.Quit();
    }
}

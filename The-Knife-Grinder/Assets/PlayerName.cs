using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerName : NetworkBehaviour
{
    public TextMesh playerNameText;
    public GameObject floatingInfo;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName; 
    }

    void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
    }

    public override void OnStartLocalPlayer()
    {
        floatingInfo.transform.localPosition = new Vector3(0, -0.3f, 0.6f);
        floatingInfo.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        string name = "Player" + Random.Range(100, 999);
        Color color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );
        CmdSetupPlayer(name, color);

    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        playerName = _name;
        playerColor = _col; 
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            floatingInfo.transform.LookAt(Camera.main.transform);
            return;
        }
        else
        { 
            floatingInfo.transform.LookAt(Camera.main.transform);
        }


    }
}

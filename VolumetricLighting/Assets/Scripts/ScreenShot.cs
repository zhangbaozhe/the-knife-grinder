using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour
{
    public string Filename = "Screenshot.png";
    public int SuperSize = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeScreenShot();
        }
    }
    public void TakeScreenShot()
    {
        ScreenCapture.CaptureScreenshot(Filename, SuperSize);
        Debug.Log($"Screenshot saved to {Filename}");
    }
}

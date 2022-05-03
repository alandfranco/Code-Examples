using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOptions : MonoBehaviour, IScreen
{
    bool active;
    public UnityEngine.UI.Text text;
    string result;

    public void OnOK()
    {
        if (!active) return;
        result = "OK";
        ScreenManager.instance.Pop();
    }
    public void OnCancel()
    {
        if (!active) return;
        result = "Cancel";
        ScreenManager.instance.Pop();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        if (!active) return;
        Screen.fullScreen = isFullscreen;
    }
    public void Activate()
    {
        active = true;
    }
    public void Deactivate()
    {
        active = false;
    }

    public string Free()
    {
        Destroy(gameObject);
        return result;
    }
}

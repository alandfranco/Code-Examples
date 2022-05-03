using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class ScreenPause : MonoBehaviour, IScreen {
    bool active;
    public UnityEngine.UI.Text text;
    string result;

    public void OnReturn()
    {
        if (!active) return;
        result = "OK";
        ScreenManager.instance.Pop();
    }

    public void OnOptions()
    {
        if (!active) return;
        result = "Options";        
        ScreenManager.instance.Push("CanvasOptions");        
    }

    public void MainMenu()
    {
        if (!active) return;
        result = "MainMenu";
        SceneManager.LoadScene("Menu");
    }

    public void MainMenus()
    {
        result = "MainMenu";
        SceneManager.LoadScene("Menu");
    }

    public void Activate()
    {
        Debug.Log("Last result: " + text.text);
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

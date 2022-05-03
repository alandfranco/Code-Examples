using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IScreen {

    bool active;
    public UnityEngine.UI.Text text;
    string result;

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnCredits()
    {
        if (!FindObjectOfType<ScreenCredits>() && !FindObjectOfType<ScreenControls>())
        {
            result = "Credits";
            ScreenManager.instance.Push("ScreenCredits");
        }
    }
    
    public void OnControls()
    {
        if(!FindObjectOfType<ScreenControls>() && !FindObjectOfType<ScreenCredits>())
        {
            result = "Controls";
            ScreenManager.instance.Push("CanvasControls");
        }
    }

    public void Quit()
    {
        Application.Quit();
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

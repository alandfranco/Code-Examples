using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Config : MonoBehaviour {

    public Transform mainGameXf;//Variable to indicate the main
    
    ScreenManager _mgr;//Screen Manager

	void Start ()
    {        
        _mgr = GetComponent<ScreenManager>();
        _mgr.Push(new ScreenGO(mainGameXf));//Assign base screen
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !FindObjectOfType<ScreenPausa>())
        {
            var s = Instantiate(Resources.Load<ScreenPause>("CanvasPause"));//We create the screen             
            _mgr.Push(s);//We add the screen we created on the stack
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _mgr.Pop();//We desactivate the last screen
        }
    }
}

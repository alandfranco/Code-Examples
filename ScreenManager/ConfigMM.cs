using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfigMM : MonoBehaviour
{

    public Transform mainGameXf;
    //public Transform overlayGameXf;
    ScreenManager _mgr;

    void Start()
    {
        _mgr = GetComponent<ScreenManager>();
        _mgr.Push(new ScreenGO(mainGameXf));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _mgr.Pop();
        }
    }
}

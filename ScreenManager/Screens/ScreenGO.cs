using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScreenGO : IScreen
{
    //Dictionary to know the behaviour state (active or not)
    Dictionary<Behaviour, bool> _before = new Dictionary<Behaviour, bool>();
    public Transform root;

    public ScreenGO(Transform root)
    {
        this.root = root;
    }

    public void Activate()
    {
        Time.timeScale = 1;
         foreach(var kv in _before)
         {
             kv.Key.enabled = kv.Value;
         }
         _before.Clear();
    }

    public void Deactivate()
    {
        foreach (var b in root.GetComponentsInChildren<Behaviour>())
        {
            _before[b] = b.enabled;
            b.enabled = false;
        }   
        Time.timeScale = 0;
    }

    public string Free()
    {
        GameObject.Destroy(root.gameObject);
        return "";
    }
}

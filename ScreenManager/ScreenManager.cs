using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScreenManager : MonoBehaviour
{
    Stack<IScreen> _stack;

    public string lastResult;

    static public ScreenManager instance;

    void Awake ()
    {
        _stack = new Stack<IScreen>();
        instance = this;
    }

    public void Pop()
    {
        if (_stack.Count <= 1)
            return;

        lastResult = _stack.Pop().Free();//The screen knows how to be free

        if (_stack.Count > 0)
            _stack.Peek().Activate();//The screen knonws how to activate
    }

    public void Push(IScreen screen)
    {
        if (_stack.Count > 0)
            _stack.Peek().Deactivate();//The screen knonws how to deactivate

        _stack.Push(screen);
        screen.Activate();
    }

    public void Push(string resource)
    {
        var go = Instantiate(Resources.Load<GameObject>(resource));
        Push(go.GetComponent<IScreen>());
    }
}

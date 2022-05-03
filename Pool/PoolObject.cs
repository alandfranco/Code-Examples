using System.Collections;
using UnityEngine;

public class PoolObject<T>
{
    private bool _isActive;
    private T _obj;
    public delegate void PoolCallBack(T Obj);

    private PoolCallBack _initializationCallback;
    private PoolCallBack _finalizationCallback;

    //We set the object parameter
    public PoolObject(T obj, PoolCallBack initialization, PoolCallBack finalization)
    {
        _obj = obj;
        _initializationCallback = initialization;
        _finalizationCallback = finalization;
        _isActive = false;
    }

    //Return the obj
    public T GetObj
    {
        get
        {
            return _obj;
        }
    }

    //Check the Initialization and Finalization
    public bool isActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
            if (_isActive)
            {
                if (_initializationCallback != null)
                    _initializationCallback(_obj);
            }
            else
            {
                if (_finalizationCallback != null)
                    _finalizationCallback(_obj);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> {

    private List<PoolObject<T>> _poolList;
    public delegate T CallbackFactory();

    private int _count;
    private bool _isDinamic = false;
    private PoolObject<T>.PoolCallBack _init;
    private PoolObject<T>.PoolCallBack _finalize;
    private CallbackFactory _factoryMethod;
            
    //Constructor that is in charge of saving the necesary references to create the object that we want to pool
    //Ask for a initial stock amount, and how to create every object, its functions of Initilization and Finalization
    //Last we set if the object is Dynamic (if we want to create new objects if every other is in use)    
    public Pool(int initialStock, CallbackFactory factoryMethod, PoolObject<T>.PoolCallBack Initialize,
                PoolObject<T>.PoolCallBack finalize, bool isDinamic)
    {        
        //List with the in the pool
        _poolList = new List<PoolObject<T>>();
                
        //We save the references for when we need it
        _factoryMethod = factoryMethod;
        _isDinamic = isDinamic;
        _count = initialStock;
        _init = Initialize;
        _finalize = finalize;

        //We generate the initial stock
        for (int i = 0; i < _count; i++)
        {
            _poolList.Add(new PoolObject<T>(_factoryMethod(), _init, _finalize));
            _poolList[i].isActive = false; //Deactivate the objects so they don't appear on screen
        }
    }

    //We search for the first object that isn't beign use
    //If we don't find any and the pool is dynamic, we ask for a new one    
    public T GetObjectFromPool()
    {
        for (int i = 0; i < _poolList.Count; i++)
        {
            if (!_poolList[i].isActive)
            {
                _poolList[i].isActive = true;
                return _poolList[i].GetObj;
            }
        }
        if (_isDinamic)
        {
            PoolObject<T> newPoolObj = new PoolObject<T>(_factoryMethod(), _init, _finalize);
            newPoolObj.isActive = true;
            _poolList.Add(newPoolObj);
            _count++;
            return newPoolObj.GetObj;
        }
        return default(T);
    }

    //Function that disables an object
    //We search the list for the object we are looking for    
    //If we find it, we disable it
    public void DisablePoolObject(T obj)
    {
        foreach (PoolObject<T> poolObj in _poolList)
        {
            if (poolObj.GetObj.Equals(obj))
            {
                poolObj.isActive = false;
                return;
            }
        }
    }
}

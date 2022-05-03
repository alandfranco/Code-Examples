using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {

    #region Variables para el Pool
    public int amount;
    public Bullet prefab;
    private Pool<Bullet> _bulletPool;

    private static BulletSpawner _instance;
    public static BulletSpawner Instance { get { return _instance; } }
    #endregion

    void Awake()
    {
        _instance = this;

        //We create a Pool of T (bullets), we set the initial stock
        //And we tell it how to create the objects (bulletFactory)
        //We indicate the Initialize and Dispose function
        //We set if is dynamic or not the pool
        _bulletPool = new Pool<Bullet>(amount, BulletFactory, Bullet.InitializeBullet, Bullet.DisposeBullet, true);
    }

    void Update()
    {

    }

    #region Pool Functions 
    //I call the GetObject to ask for objs to the pool and im going to return it
    public Bullet GetObject()
    {
        return _bulletPool.GetObjectFromPool();
    }

    //Factory of we create the bullet
    private Bullet BulletFactory()
    {
        return Instantiate<Bullet>(prefab);
    }

    //The spawner is the only one that knows the pool
    //So we receive an object by parameter and we send it as a parameter to the pool
    //That is in charge to desactivate it in its list, so we can use it again later
    public void ReturnBulletToPool(Bullet bullet)
    {
        _bulletPool.DisablePoolObject(bullet);
    }
    #endregion
}

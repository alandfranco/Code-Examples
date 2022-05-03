using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Vector3 initialScale;

    private float _lifeSpanTimer;

    IPattern currentStrategy;

    void Awake()
    {
        initialScale = this.transform.localScale;
        this.transform.parent = FlyWeightPointer.parent.parent;
    }

    void Update()
    {
        _lifeSpanTimer += Time.deltaTime;
        if (_lifeSpanTimer >= FlyWeightPointer.BulletState.bulletLifeSpan)
        {
            _lifeSpanTimer = 0;
            BulletSpawner.Instance.ReturnBulletToPool(this);
        }
        else if (currentStrategy != null) currentStrategy.Pattern();
    }

    public void Reset()
    {
        this.transform.localScale = initialScale;
        _lifeSpanTimer = 0;
    }

    #region Builder Functions
    public Bullet SetSize(float scalar)
    {
        this.gameObject.transform.localScale *= scalar;
        return this;
    }

    public Bullet SetColor(Color color)
    {
        this.gameObject.GetComponent<Renderer>().material.color = color;
        return this;
    }

    public Bullet SetParentLayer(GameObject parent)
    {
        this.gameObject.layer = parent.gameObject.layer;
        return this;
    }

    public Bullet SetStrategy(IPattern pattern)
    {
        currentStrategy = pattern;
        return this;
    }
    #endregion

    #region PoolObject Functions
    public void Initialize()
    {       
        Reset();
        transform.position = Vector3.zero;
    }

    public void Dispose() { }
       
    public static void InitializeBullet(Bullet bulletObj)
    {
        bulletObj.gameObject.SetActive(true);
        bulletObj.Initialize();
    }

    public static void DisposeBullet(Bullet bulletObj)
    {
        bulletObj.Dispose();
        bulletObj.gameObject.SetActive(false);
    }
    #endregion
}

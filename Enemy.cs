using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
        
    IPattern currentStrategy;
    IFireMode currentFireMode;

    [HideInInspector]
    public GameObject target;

    private float currentHp;                //takes maxHp from flyweight
    public float fireRate;                  //takes from flyweight
    private float _fireRateCounter = 0f;

    private Color originalColor;

    public int score;

    private Vector3 initialScale;

    ViewPlayer view;

    void Awake()
    {
        view = FindObjectOfType<ViewPlayer>();
        initialScale = this.transform.localScale;
        target = FindObjectOfType<Hero>().gameObject;
        this.transform.parent = FlyWeightPointer.parent.parent;
    }

    void Update()
    {
        if (currentStrategy != null) currentStrategy.Pattern();

        _fireRateCounter += Time.deltaTime;
        if (_fireRateCounter >= fireRate)
        {
            _fireRateCounter = 0;
            if (currentFireMode != null) currentFireMode.FireMode();        //Shooting depends on current fire mode
        }
    }


    public void Reset()
    {
        this.transform.localScale = initialScale;
        _fireRateCounter = 0;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<EnemyBoundaries>()) EnemySpawner.Instance.ReturnEnemyToPool(this);

        if (c.gameObject.GetComponent<Bullet>())
        {
            TakeDamage();                                                           //Apply damage
            BulletSpawner.Instance.ReturnBulletToPool(c.GetComponent<Bullet>());    //give back the bullet to the pool
        }
    }

    public void TakeDamage()
    {
        currentHp--;
        StartCoroutine(DamageFeedBack());

        if (currentHp <= 0)
        {
            view.RepaintPoints(score);
            EnemySpawner.Instance.ReturnEnemyToPool(this);      //give back enemy to the pool
        }
    }

    IEnumerator DamageFeedBack()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<Renderer>().material.color = originalColor;

    }

    #region Funciones de Builder
    //Take maxHp from flyweight
    public Enemy SetLife(int maxHp)
    {
        currentHp = maxHp;
        return this;
    }

    //Set the movement Strategy
    public Enemy SetStrategy(IPattern pattern)
    {
        currentStrategy = pattern;
        return this;
    }

    //Set the attack strategy
    public Enemy SetFireMode(IFireMode mode)
    {
        currentFireMode = mode;
        return this;
    }

    //Set speed attack
    public Enemy SetFireRate(float attackRate)
    {
        fireRate = attackRate;
        return this;
    }

    //Set size
    public Enemy SetSize(float scalar)
    {
        this.gameObject.transform.localScale *= scalar;
        return this;
    }

    //Set color by enemy
    public Enemy SetColor(Color color)
    {
        originalColor = color;
        this.gameObject.GetComponent<Renderer>().material.color = color;
        return this;
    }
    
    //Set the amount of points an enemy gives
    public Enemy SetScore(int points)
    {
        score = points;
        return this;
    }
    #endregion

    #region Funciones de PoolObject
    public void Initialize()
    {
        Reset();
    }

    public void Dispose() { }

    public static void InitializeEnemy(Enemy enemyObj)
    {
        enemyObj.gameObject.SetActive(true);
        enemyObj.Initialize();
    }

    public static void DisposeEnemy(Enemy enemyObj)
    {
        enemyObj.Dispose();
        enemyObj.gameObject.SetActive(false);
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //para ordenar el enum
using System;

public class EnemySpawner : MonoBehaviour {

    #region Pool Variables
    public int amount;
    public Enemy prefab;
    private Pool<Enemy> _enemyPool;

    private static EnemySpawner _instance;
    public static EnemySpawner Instance { get { return _instance; } }
    #endregion

    private List<Transform> spawnPoints = new List<Transform>();
    public float spawnDelay = 3;
    private float _spawnTime;
    private float _generalTimer = 0;
    private float _enemiesPerWave = 1;
    private float _maxEnemiesPerWave = 4;
    private int _increaseDifficultyTime = 20;

    private enum enemyType { normal, zigzager, chaser }

    void Awake()
    {
        _instance = this;

        _enemyPool = new Pool<Enemy>(amount, EnemyFactory, Enemy.InitializeEnemy, Enemy.DisposeEnemy, true);

        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
    }
	
	void Update () {

        SpawnEnemies();
        IncreaseEnemyCount();

	}

    public void IncreaseEnemyCount()
    {
        _generalTimer += Time.deltaTime;

        if ((int)_generalTimer/ _increaseDifficultyTime > 0 && (int)_generalTimer / _increaseDifficultyTime >= _enemiesPerWave)
        {
            if (_enemiesPerWave < _maxEnemiesPerWave)
            {
                _enemiesPerWave++;
                spawnDelay+= .5f;
            }
        }
    }

    public void SpawnEnemies()
    {
        _spawnTime += Time.deltaTime;

        if (_spawnTime >= spawnDelay)
        {
            for (int i = 0; i < _enemiesPerWave; i++)
            {
                _spawnTime = 0;
                Enemy newEnemy = GetObject();           
                SetEnemyType(newEnemy);                 
                int random = UnityEngine.Random.Range(0, spawnPoints.Count);    
                newEnemy.transform.position = spawnPoints[random].position;     
                newEnemy.transform.forward = spawnPoints[random].forward;       
            }
        }
    }

    public void SetEnemyType(Enemy newEnemy)
    {
        int max = Enum.GetValues(typeof(enemyType)).Cast<int>().Max();
        enemyType type = (enemyType)UnityEngine.Random.Range(0, max+1);

        switch (type)
        {
            case enemyType.normal:
                newEnemy.SetLife(FlyWeightPointer.NormalEnemyState.maxHp);
                newEnemy.SetStrategy(new NormalPattern(newEnemy.transform, FlyWeightPointer.NormalEnemyState.speed));
                newEnemy.SetFireMode(new ShotgunMode(newEnemy.transform, 3, 7f));
                newEnemy.SetFireRate(FlyWeightPointer.NormalEnemyState.fireRate);
                newEnemy.SetColor(FlyWeightPointer.NormalEnemyState.color);
                newEnemy.SetScore(FlyWeightPointer.NormalEnemyState.score);
                break;

            case enemyType.chaser:
                newEnemy.SetLife(FlyWeightPointer.ChaserEnemyState.maxHp);
                newEnemy.SetStrategy(new TargetPattern(newEnemy.transform, FlyWeightPointer.ChaserEnemyState.speed, newEnemy.target.transform));
                newEnemy.SetFireMode(new AutofireMode(newEnemy.transform));
                newEnemy.SetFireRate(FlyWeightPointer.ChaserEnemyState.fireRate);
                newEnemy.SetColor(FlyWeightPointer.ChaserEnemyState.color);
                newEnemy.SetScore(FlyWeightPointer.ChaserEnemyState.score);
                break;

            case enemyType.zigzager:
                newEnemy.SetLife(FlyWeightPointer.ZigzagerEnemyState.maxHp);
                newEnemy.SetStrategy(new SinousPattern(newEnemy.transform, FlyWeightPointer.ZigzagerEnemyState.speed, 5f));
                newEnemy.SetFireMode(new NovaMode(newEnemy.transform, 10));
                newEnemy.SetFireRate(FlyWeightPointer.ZigzagerEnemyState.fireRate);
                newEnemy.SetColor(FlyWeightPointer.ZigzagerEnemyState.color);
                newEnemy.SetScore(FlyWeightPointer.ZigzagerEnemyState.score);
                break;
        }
    }

    #region Pool functions
    
    public Enemy GetObject()
    {
        return _enemyPool.GetObjectFromPool();
    }
       
    private Enemy EnemyFactory()
    {
        return Instantiate<Enemy>(prefab);
    }
    
    public void ReturnEnemyToPool(Enemy enemy)
    {
        _enemyPool.DisablePoolObject(enemy);
    }
    #endregion
}

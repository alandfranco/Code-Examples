using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlyWeightPointer {

    //We create a static variable from the "Structure" that we created and we assign it values
    //we assign the readonly property so it cant we change from outside
    //inside it we set the values to the variables    

    public static readonly FlyWeight NormalEnemyState = new FlyWeight
    {
        maxHp = 4,
        speed = 5f,
        fireRate = 1.5f,
        score = 100,
        color = new Color(1, 0, 0),
    };

    public static readonly FlyWeight ZigzagerEnemyState = new FlyWeight
    {
        maxHp = 3,
        speed = 3f,
        fireRate = 2f,
        score = 150,
        color = new Color(.8f, 0, .5f),
    };

    public static readonly FlyWeight ChaserEnemyState = new FlyWeight
    {
        maxHp = 6,
        speed = 3f,
        fireRate = 1f,
        score = 200,
        color = new Color(.8f, .5f, 0),
    };

    public static readonly FlyWeight BulletState = new FlyWeight
    {
        normalBulletSpeed = 12f,
        shotgunBulletSpeed = 9f,
        novaBulletSpeed = 5f,
        bulletLifeSpan = 2f,
};

    public static readonly FlyWeight PowerUp = new FlyWeight
    {
        speed = 50f,
        score = 100,
    };

    public static readonly FlyWeight parent = new FlyWeight
    {
        parent = GameObject.Find("Parent").transform,
    };
    //We access the variables from the flyweight applying them directly
    //For example, inside the script Enemy it will look like this    
    //void Update()
    //{
        //transform.position += FlyWeightPointer.State.speed * FlyWeightPointer.State.dir * Time.deltaTime;
    //}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutofireMode : IFireMode
{
    Transform obj;

    public AutofireMode(Transform shooter)
    {
        obj = shooter;
    }

    public void FireMode()
    {
        Bullet newBullet = BulletSpawner.Instance.GetObject();              //Ask the pool for a bullet
        SetBulletValues(newBullet);                                         //Set the variables of the bullet with a builder
        newBullet.transform.position = obj.position;                        
        newBullet.transform.forward = obj.forward;                          
    }

    public void SetBulletValues(Bullet bullet)
    {
        bullet.SetParentLayer(this.obj.transform.gameObject);
        bullet.SetStrategy(new NormalPattern(bullet.transform, FlyWeightPointer.BulletState.normalBulletSpeed));  //speed from the flyweight
        bullet.SetColor(obj.GetComponent<Renderer>().material.color);
    }
}

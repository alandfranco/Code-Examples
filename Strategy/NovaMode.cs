using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaMode : IFireMode
{
    int bulletsNum;
    Transform obj;

    public NovaMode(Transform shooter ,int bulletCount)
    {
        bulletsNum = bulletCount;
        obj = shooter;
    }

    public void FireMode()
    {
        for (int i = 0; i < bulletsNum; i++)
        {
            Bullet newBullet = BulletSpawner.Instance.GetObject();              //Ask the pool for a bullet
            SetBulletValues(newBullet);                                         //Set the variables of the bullet with a builder
            newBullet.transform.position = obj.position;                        
            newBullet.transform.forward = obj.forward;
            newBullet.transform.localEulerAngles = new Vector3(0, (360 / bulletsNum) * i, 0);
        }
    }

    public void SetBulletValues(Bullet bullet)
    {

        bullet.SetParentLayer(this.obj.transform.gameObject);
        bullet.SetStrategy(new SinousPattern(bullet.transform, FlyWeightPointer.BulletState.novaBulletSpeed, 15f)); //speed from flyweight
        bullet.SetColor(obj.GetComponent<Renderer>().material.color);
    }
}

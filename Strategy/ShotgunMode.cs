using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMode : IFireMode
{
    int bulletsNum;
    float spread;
    Transform obj;

    public ShotgunMode(Transform shooter, int bulletCount, float angle)
    {
        obj = shooter;
        bulletsNum = bulletCount;
        spread = angle;
    }

    public void FireMode()
    {
        for (int i = 0; i < bulletsNum; i++)
        {
            Bullet newBullet = BulletSpawner.Instance.GetObject();              
            SetBulletValues(newBullet);                                         
            newBullet.transform.position = obj.position;                        
            newBullet.transform.forward = obj.forward;                          
            newBullet.transform.localEulerAngles = new Vector3(0, obj.localEulerAngles.y + ((spread) * i) - spread, 0);
        }
    }

    public void SetBulletValues(Bullet bullet)
    {
        bullet.SetParentLayer(this.obj.transform.gameObject);
        bullet.SetStrategy(new NormalPattern(bullet.transform, FlyWeightPointer.BulletState.shotgunBulletSpeed));
        bullet.SetColor(obj.GetComponent<Renderer>().material.color);
    }
}

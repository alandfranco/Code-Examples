using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeight{

    public int maxHp;
    public float speed;
    public float fireRate;
    public int score;
    public Color color;

    public Transform parent;

    #region Bullet
    public float bulletLifeSpan;
    public float normalBulletSpeed;
    public float shotgunBulletSpeed;
    public float novaBulletSpeed;
    #endregion
}

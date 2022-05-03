using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModelPlayer
{
    Transform player;

    public int currentHp;
    public int maxHp;

    public int specialAmmo;
    public int specialAmmoMax;

    public float speed;
    public float fireRate;

    Vector3 lookDir;

    public IFireMode currentFireMode;
    public IFireMode autoFireMode;
    public IFireMode shotgunFireMode;
    public IFireMode specialFireMode;

    public ModelPlayer(Transform t)
    {
        player = t;
    }

    public void OnMove(Vector3 newPos)
    {
        player.Translate(newPos * Time.deltaTime * speed, Space.World);
    }

    public void LookAtMouse(Vector3 mousePos)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * player.position.y);
        float rayDist;

        if (groundPlane.Raycast(ray, out rayDist))
        {
            Vector3 point = ray.GetPoint(rayDist);
            lookDir = point - player.position;
            lookDir.y = 0;
        }
        player.forward = Vector3.Lerp(player.forward, lookDir, .1f);
    }

    public void Shoot()
    {        
        //To change the fire mode we have to change the IFireMode assigned to currentFireMode
        if (currentFireMode != null) currentFireMode.FireMode();
    }

    public void SpecialAttack()
    {
        if (specialFireMode != null && specialAmmo > 0)
        {
            specialAmmo--;
            specialFireMode.FireMode();
            player.GetComponent<Hero>().view.RepaintAmmo(specialAmmo);
        }
    }
        
    public void TakeDamage()
    {
        if (currentHp <= 0)
        {
            GameManager.YouLoose();
            currentHp = 0;
        }
        else
        {
            currentHp--;
        }
    }

    public void Heal(int amount)
    {
        currentHp += amount;
        if (currentHp >= maxHp) currentHp = maxHp;

        player.GetComponent<Hero>().view.RepaintLife(currentHp);
    }

    public void GiveAmmo()
    {
        specialAmmo++;
        player.GetComponent<Hero>().view.RepaintAmmo(specialAmmo);
    }

    public void GivePoints(int score)
    {
        player.GetComponent<Hero>().view.RepaintPoints(score);
    }
}

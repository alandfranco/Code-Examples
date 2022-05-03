using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUp : PowerUpItem, IPowerUp
{
    string msg = "You found Special Ammo!";

    public void Effect(ModelPlayer player)
    {
        if (player.specialAmmo < player.specialAmmoMax)
        {
            player.GiveAmmo();
            Destroy(this.gameObject);
        }
    }

    public string Message()
    {
        return msg;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUp : PowerUpItem, IPowerUp
{
    string msg = "Received Healing!";

    public void Effect(ModelPlayer player)
    {
        if (player.currentHp < player.maxHp)
        {
            player.Heal(25);
            Destroy(this.gameObject);
        }
    }

    public string Message()
    {
        return msg;
    }
}

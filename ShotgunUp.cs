using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUp : PowerUpItem, IPowerUp
{
    string msg = "OMFG! thats a Shotgun!";

    public void Effect(ModelPlayer player)
    {
        if (player.currentFireMode != player.shotgunFireMode)
        {
            player.currentFireMode = player.shotgunFireMode;
        }
        else
        {
            msg = "You dont need another shotgun! Have some points instead!";
            player.GivePoints(FlyWeightPointer.PowerUp.score);
        }
        Destroy(this.gameObject);
    }

    public string Message()
    {
        return msg;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewPlayer : MonoBehaviour {

    [HideInInspector]
    public Hero player;
    [HideInInspector]
    public ModelPlayer model;

    public TextMesh myName;
    public Text myLife;
    public Image HealthBar;
    public Text currentAmmo;
    public Text popUpText;
    public Text pointsText;
    public Text timeLeft;

    void Awake()
    {
        pointsText.text = GameManager.totalPoints.ToString();
        HealthBar.fillAmount = 1;
        popUpText.text = "";
    }

    public void RepaintLife(int life)
    {
        myLife.text = life.ToString();

        HealthBar.fillAmount = ((float)life / model.maxHp);
    }

    public void RepaintAmmo(int ammo)
    {
        currentAmmo.text = "Special ammo: " + ammo.ToString();
    }

    public void RepaintPoints(int point)
    {
        GameManager.totalPoints += point; 
        pointsText.text = "Points: " + GameManager.totalPoints.ToString();
    }

    public void RepaintTimeLeft(float time)
    {
        timeLeft.text = "Time Left: " + (int)time;
    }

    public void RepaintPopUpText(string text)
    {
        popUpText.text = text;
        StartCoroutine(EraseText());
    }

    IEnumerator EraseText()
    {
        yield return new WaitForSeconds(3);
        popUpText.text = "";
    }
}

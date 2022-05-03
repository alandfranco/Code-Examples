using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int totalPoints = 0;

    public static float time;
    public float maxTime = 120;
    ViewPlayer view;    

    public void Awake()
    {
        totalPoints = 0;
        view = FindObjectOfType<ViewPlayer>();
        time = maxTime;
        view.RepaintPoints(totalPoints);
    }

    public void Update()
    {
        time -= Time.deltaTime;

        view.RepaintTimeLeft(time);

        if(time <= 0)
        {            
            SceneManager.LoadScene("Win");
        }
    }

    public static void YouLoose()
    {        
        SceneManager.LoadScene("Loose");
    }

}

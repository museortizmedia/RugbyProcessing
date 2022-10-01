using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puntaje : MonoBehaviour
{
    private float StartTime;
    void Start()
    {
        StartTime = Time.time;
    }

    void Update()
    {
        if (!gameController.instancia.gameover)
        {
            float TimerControl = Time.time - StartTime;
            string segs = (TimerControl % 60).ToString("00");
            gameController.instancia.puntaje = int.Parse(segs);
            GetComponent<TMP_Text>().text = segs;
        }
        else { GetComponent<TMP_Text>().text = "0"; }
        
    }
}

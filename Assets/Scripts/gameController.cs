using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public static gameController instancia { get; private set; }
    public int puntaje;
    public bool gameover=false;

    public GameObject Player;
    public GameObject gameoverScreen;
    public GameObject BG;
    private void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(this);
        } else { instancia = this; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (gameover)
        {
            Time.timeScale = 0;
            gameoverScreen.SetActive(true);
        }
    }
}

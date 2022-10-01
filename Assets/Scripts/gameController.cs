using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public static gameController instancia { get; private set; }
    public int puntaje;
    public bool gameover=false;

    public GameObject Player;
    public GameObject EnemiesSpawner;
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
        gameoverScreen.SetActive(false);
    }

    void Update()
    {
        if (gameover)
        {
            //sonido tacle exitoso
            for (var i = EnemiesSpawner.transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(EnemiesSpawner.transform.GetChild(i).gameObject);
            }
Destroy(EnemiesSpawner);
            Player.GetComponent<Animator>().SetBool("muerte", true);
            Player.GetComponent<AudioSource>().Play();
            GameOver();            
        }
    }
    public void GameOver() {
        Time.timeScale = 0.2f;
        gameoverScreen.SetActive(true);
        gameController.instancia.gameover = false;
    }

    public void VolverBTN()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitBTN()
    {
        Application.Quit();
    }
    public void Shake() { BG.GetComponent<Animator>().SetTrigger("shake"); }
}

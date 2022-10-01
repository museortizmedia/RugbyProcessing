using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tacle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {            
            gameController.instancia.gameover=true;
        }
    }
}

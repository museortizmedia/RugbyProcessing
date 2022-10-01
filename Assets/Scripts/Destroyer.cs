using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    public GameObject tacleadores;
    public float lifetime;

    private void Start()
    {
        lifetime += Time.time;
        tacleadores = GameObject.Find("Tacleadores");
    }
    private void Update()
    {
        if (lifetime < Time.time)
        {
            tacleadores.GetComponent<tacleadores>().SpawnearTacle(transform.position);
            Destroy(gameObject);           
        }
    }
}

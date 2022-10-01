using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tacleadores : MonoBehaviour
{
    [SerializeField] GameObject tacleobj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnearTacle(Vector3 laposition)
    {
        GameObject tacle = Instantiate(tacleobj);
        tacle.transform.position = new Vector3(laposition.x, 44.5f, 0);
        tacle.transform.SetParent(gameObject.transform);
        Destroy(tacle, 1.2f);
    }
}

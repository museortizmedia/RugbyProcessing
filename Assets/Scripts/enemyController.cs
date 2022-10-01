using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] float Scalespeed = 0.02f;
    [SerializeField] float Runspeed = 0.003f;
    [SerializeField] Vector3 finalScale = new Vector3(1,1,1);

    private RectTransform objectRectTransform;
    private Vector2 position;
    float randX;

    private void OnEnable()
    {
        objectRectTransform = GetComponent<RectTransform>();
        position = objectRectTransform.anchoredPosition;
        randX = Random.Range(0, Screen.width);
    }

    void Start()
    {
        position = new Vector2(0, -50);
        transform.position = new Vector3(randX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        float posY = transform.position.y - Runspeed;
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        transform.localScale = Vector3.Lerp(transform.localScale, finalScale, Scalespeed * Time.deltaTime);
    }
}

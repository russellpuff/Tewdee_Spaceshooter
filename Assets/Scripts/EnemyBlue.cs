using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyBlue : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] GameManager manager;

    void Start()
    {

    }

    void Update()
    {
        transform.position -= new Vector3(0, speed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        Destroy(collision.gameObject);

        if (collision.gameObject.CompareTag("Player")) { GameManager.instance.InitiateGameOver(); }
        else { GameManager.instance.IncreaseScore(10); } // Enemy destroyed

        
        
    }
}

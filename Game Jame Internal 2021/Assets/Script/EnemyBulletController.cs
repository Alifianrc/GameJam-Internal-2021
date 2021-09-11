using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private GameManager manager;
    private float forceLeft = -1250;
    private float forceUp = 350;
    private Transform bulletDestroyer; 

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        bulletDestroyer = manager.enemyBulletDestroyer.transform;

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(new Vector2(forceLeft, forceUp));
    }

    private void Update()
    {
        if(transform.position.x < bulletDestroyer.position.x || transform.position.y < bulletDestroyer.position.y)
        {
            Destroy(gameObject);
        }
    }
}

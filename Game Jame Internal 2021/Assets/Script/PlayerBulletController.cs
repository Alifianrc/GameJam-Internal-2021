using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private float bulletSpeed = 15f;
    private GameObject bulletDestoryer;

    private void Start()
    {
        bulletDestoryer = FindObjectOfType<GameManager>().playerBulletDestroyer;
    }

    private void Update()
    {
        if(bulletDestoryer.transform.position.x > transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + bulletSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

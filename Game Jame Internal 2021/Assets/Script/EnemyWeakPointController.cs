using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeakPointController : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private GameManager manager;

    private int livePoint;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        livePoint = Random.Range(3, 6);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            livePoint--;
            Destroy(collision.gameObject);
            if(livePoint <= 0)
            {
                manager.EnemyKilled();
                Destroy(parent);
            }
        }
    }
}

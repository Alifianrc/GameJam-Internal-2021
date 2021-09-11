using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    private Vector2 minPosCamera;
    private Vector2 maxPosCamera;
    private float offset = .9f;
    private Vector2 targetPos;
    private float distaceLimit = .4f;

    private float speed = .005f;
    private float fireDelay = 5;
    [SerializeField] private Transform firePoint;

    void Start()
    {
        minPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        maxPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

        float randX = Random.Range(minPosCamera.x + ((maxPosCamera.x - minPosCamera.x) / 2) + offset, maxPosCamera.x - offset);
        float randY = Random.Range(minPosCamera.y + offset, maxPosCamera.y + offset);
        targetPos = new Vector2(randX, randY);

        StartCoroutine(FireBullet());
    }

    private IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            fireDelay = Random.RandomRange(4, 8);
            Instantiate(bullet, firePoint.position, Quaternion.identity);
        }
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - targetPos.x) > distaceLimit && Mathf.Abs(transform.position.y - targetPos.y) > distaceLimit)
        {
            transform.position = new Vector2(Mathf.Lerp(this.transform.position.x, targetPos.x, speed), Mathf.Lerp(this.transform.position.y, targetPos.y, speed));
        }
        else
        {
            float randX = Random.Range(minPosCamera.x + ((maxPosCamera.x - minPosCamera.x) * .75f) + offset, maxPosCamera.x - offset);
            float randY = Random.Range(minPosCamera.y + offset, maxPosCamera.y + offset);
            targetPos = new Vector2(randX, randY);

            speed = Random.Range(.0009f, .0055f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
        }
    }
}

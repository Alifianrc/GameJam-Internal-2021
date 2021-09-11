using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Joystick joystick;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    private GameManager manager;

    private float maxSpeed = 7f;
    private float speed;

    private float maxFireDelay = .2f;
    private float fireDelay;

    private Vector2 minPosCamera;
    private Vector2 maxPosCamera;
    private float offset = .7f;

    private int livePoint = 3;
    private bool IsDead;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();

        joystick = FindObjectOfType<Joystick>();
        speed = maxSpeed;
        fireDelay = maxFireDelay;

        minPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        maxPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

        StartCoroutine(FireBullet());
    }

    
    void Update()
    {
        if (!IsDead)
        {
            Vector2 move = new Vector2(
            transform.position.x + joystick.Horizontal * speed * Time.deltaTime,
            transform.position.y + joystick.Vertical * speed * Time.deltaTime);
            if (move.x - offset > minPosCamera.x && move.x + offset < maxPosCamera.x)
            {
                transform.position = new Vector3(move.x, transform.position.y, -1);
            }
            if (move.y - offset > minPosCamera.y && move.y + offset < maxPosCamera.y)
            {
                transform.position = new Vector3(transform.position.x, move.y, -1);
            }

            if(transform.position.x - offset < minPosCamera.x || transform.position.x + offset > maxPosCamera.x)
            {
                transform.position = new Vector3(-7, 0, -1);
            }
            else if (transform.position.y - offset < minPosCamera.y || transform.position.y + offset > maxPosCamera.y)
            {
                transform.position = new Vector3(-7, 0, -1);
            }
        }
    }

    private IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireDelay);
            if (!IsDead)
            {
                Instantiate(bullet, firePoint.position, Quaternion.identity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !IsDead)
        {
            livePoint--;
            IsDead = true;
            if(livePoint > 0)
            {
                StartCoroutine(PlayerIsDead());
            }
            else
            {
                manager.GameIsOver();
            }
        }
        Debug.Log(collision.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" && !IsDead)
        {
            livePoint--;
            IsDead = true;
            if (livePoint > 0)
            {
                StartCoroutine(PlayerIsDead());
            }
            else
            {
                manager.GameIsOver();
            }
        }
    }

    private IEnumerator PlayerIsDead()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 1, 1, 0);

        transform.position = new Vector3(-7, 0, -1);

        yield return new WaitForSeconds(3);

        collider.enabled = true;
        renderer.color = new Color(1, 1, 1, 1);
        IsDead = false;
    }
}

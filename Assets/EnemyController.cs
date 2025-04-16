using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform spawner;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject ex;
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Canvas canvas;
    [SerializeField] Slider slider;
    [SerializeField] float rotateSpeed;
    [SerializeField] float sensY;
    [SerializeField] float sensX;

    float health = 100;

    bool canShoot = true;

    bool canRotate = true;

    private void Start()
    {
        player = GameObject.Find("PlayerTank");
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Move();
        Rotate();
        CanvasRotate();
        StartCoroutine(Shoot());
    }

    private void Move()
    {
        agent.SetDestination(player.transform.position);
    }

    void Rotate()
    {
        if (canRotate)
        {
            Vector3 dir = Vector3.RotateTowards(Vector3.forward, player.transform.position - transform.position, rotateSpeed, 0);

            turret.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    IEnumerator Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            canRotate = false;
            yield return new WaitForSeconds(1);
            Instantiate(bullet, spawner.position, spawner.rotation);
            GameObject g = Instantiate(smoke, spawner.position, spawner.rotation);
            yield return new WaitForSeconds(1);
            canRotate = true;
            yield return new WaitForSeconds(2);
            Destroy(g);
            yield return new WaitForSeconds(3);
            canShoot = true;
        }
    }

    void CanvasRotate()
    {
        Vector3 dir = Vector3.RotateTowards(Vector3.forward, player.transform.position - transform.position, rotateSpeed, 0);
        canvas.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 50;
            slider.value = health;
            Instantiate(ex, transform.position, Quaternion.identity);

            if (health <= 0)
            {
                PlayerController.health += 20;
                Manager.score += 1;
                Manager.destroyed += 1;
                Destroy(this.gameObject);
            }
        }
    }
}


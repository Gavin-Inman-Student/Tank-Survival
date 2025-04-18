using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] float speed;
    public static float shootingTime = 3;

    float dist;

    float health = 100;

    bool canShoot = true;
    bool isShooting = false;

    bool canRotate = true;

    bool damaged = false;

    [SerializeField] bool menu;

    private void Start()
    {
        if (menu == false)
        {
            player = GameObject.Find("PlayerTank");
        }
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Move();
        Rotate();
        CanvasRotate();
        StartCoroutine(Shoot());

        dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist > 20)
        {
            canShoot = false;
        }

        else if (dist < 20 && isShooting == false)
        {
            canShoot = true;
        }
    }

    private void Move()
    {
        if (!isShooting)
        {
            agent.enabled = true;
            agent.SetDestination(player.transform.position);
        }

        else
        {
            agent.enabled = false;
        }
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
            isShooting = true;
            canShoot = false;
            canRotate = false;
            yield return new WaitForSeconds(shootingTime);
            Instantiate(bullet, spawner.position, spawner.rotation);
            GameObject g = Instantiate(smoke, spawner.position, spawner.rotation);
            yield return new WaitForSeconds(1);
            isShooting = false;
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
        if (collision.gameObject.CompareTag("Bullet") && damaged == false)
        {
            damaged = true;
            if (menu == false)
            {
                health -= 50;
            }
            slider.value = health;
            Instantiate(ex, transform.position, Quaternion.identity);

            if (health <= 0)
            {
                if (PlayerController.health + 10 <= 100)
                {
                    PlayerController.health += 10;
                }
                else if (PlayerController.health + 10 > 100)
                {
                    PlayerController.health = 100;
                }
                PlayerController.sd.value = PlayerController.health;
                Manager.score += 1;
                Manager.destroyed += 1;
                Manager.spawned -= 1;
                Destroy(this.gameObject);
            }
            StartCoroutine(Wait());           
        }

        if (collision.gameObject.CompareTag("Ghost"))
        {
            Destroy(this.gameObject);
            Manager.spawned -= 1;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        damaged = false;
    }
}


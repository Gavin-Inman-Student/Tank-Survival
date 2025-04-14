using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform spawner;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject player;
    [SerializeField] float rotateSpeed;
    [SerializeField] float sensY;
    [SerializeField] float sensX;

    bool canShoot = true;

    float mouseX;
    float mouseY;

    float movZ;
    float MovX;


    private void Start()
    {
        player = GameObject.Find("PlayerTank");
    }
    private void Update()
    {
        Move();
        Rotate();
        StartCoroutine(Shoot());
    }

    private void Move()
    {

    }

    void Rotate()
    {
        Vector3 lookDir = player.transform.position;

        lookDir.y = 0;
        
        mouseY = Mathf.Clamp(mouseY, -7f, 7f);

        turret.transform.LookAt(lookDir);

        muzzle.transform.localRotation = Quaternion.Euler(-mouseY * sensY, 0, 0);

    }

    IEnumerator Shoot()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {


            canShoot = false;
            Instantiate(bullet, spawner.position, spawner.rotation);
            GameObject g = Instantiate(smoke, spawner.position, spawner.rotation);
            yield return new WaitForSeconds(3);
            Destroy(g);
            canShoot = true;
        }
    }
}


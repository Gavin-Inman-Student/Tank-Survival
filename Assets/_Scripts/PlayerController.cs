using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform spawner; 
    [SerializeField] GameObject turret;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject smoke;
    [SerializeField] float moveSpeed;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        Move();
        Rotate();
        StartCoroutine(Shoot());
    }

    private void Move()
    {
        movZ = Input.GetAxisRaw("Vertical");
        MovX = Input.GetAxisRaw("Horizontal");
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 moveDir = new Vector3 (MovX, 0, movZ);
        moveDir.Normalize();

        rb.velocity = (transform.forward * movZ * moveSpeed) + (new Vector3 (0, -1, 0));



        if (moveDir !=  Vector3.zero)
        {
            transform.Rotate(Vector3.up, MovX * rotateSpeed * Time.deltaTime);
        }
    }

    void Rotate()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -7f, 7f);

        turret.transform.localRotation = Quaternion.Euler(0, mouseX * sensX, 0);

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

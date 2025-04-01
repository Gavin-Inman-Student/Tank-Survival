using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject turret;
    [SerializeField] GameObject body;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float sens;

    Transform orintation;

    float mouseX;
    float mouseY;

    float movZ;
    float MovX;

    private void Update()
    {
        Move();
        Rotate();
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
        mouseY = Mathf.Clamp(mouseY, -1f, 1f);

        turret.transform.localRotation = Quaternion.Euler(-mouseY * sens, mouseX * sens, 0);
       
    }
}

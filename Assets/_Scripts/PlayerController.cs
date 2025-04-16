using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform spawner; 
    [SerializeField] GameObject turret;
    [SerializeField] GameObject muzzle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject smoke;
    [SerializeField] Slider slider;
    [SerializeField] GameObject ex;
    [SerializeField] GameObject deathSmoke;
    [SerializeField] Canvas canvas;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float sensY;
    [SerializeField] float sensX;

    public static float health = 100;
    public static Slider sd;

    bool canShoot = true;

    float mouseX;
    float mouseY;

    float movZ;
    float MovX;


    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        sd = slider;
    }
    private void Update()
    {
        Move();
        Rotate();
        StartCoroutine(Shoot());
        Cursor();
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
        mouseY = Mathf.Clamp(mouseY, -30f, 30f);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 10;
            slider.value = health;
            Instantiate(ex, transform.position, Quaternion.identity);

            if (health <= 0)
            {
                deathSmoke.SetActive(true);
                Destroy(this);
            }
        }
    }

    void Cursor()
    {
        Physics.Raycast(spawner.transform.position, spawner.forward, out RaycastHit hit);

        Debug.DrawRay(spawner.transform.position, spawner.forward * 1000, Color.red);



    }
}

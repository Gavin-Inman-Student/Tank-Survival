using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    
    // Update is called once per frame
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speed;

        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10);

        Destroy(this.gameObject);
    }
}

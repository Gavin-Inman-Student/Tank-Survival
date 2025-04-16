using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static float score;
    public static float destroyed = 0;
    [SerializeField] GameObject[] spawners;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject tank;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject scoreTxt;
    [SerializeField] GameObject health;
    float spawned = 3;
    bool spawnedIn = true;
    bool active = false;
    private void Start()
    {
    }

    private void Update()
    {
        if (destroyed >= spawned)
        {
            destroyed = 0;
            spawned += 1;
            spawnedIn = true;
        }

        if (spawned == 12)
        {
            EnemyController.shootingTime = 2.5f;
        }

        if (spawned == 18)
        {
            EnemyController.shootingTime = 2;
        }

        SetText();
        Spawn();
        StartCoroutine(Menu());
    }

    void SetText()
    {
        text.SetText("score " + score);
    }

    void Spawn()
    {
        if (spawnedIn)
        {
            spawnedIn = false;
            for (int i = 0; i < spawned; i++)
            {
                int r = Random.Range(0, spawners.Length);
                Instantiate(tank, spawners[r].transform.position, Quaternion.identity);
            }
        }
    }

    IEnumerator Menu()
    {
        if (Input.GetKey(KeyCode.Tab) && !active)
        {             
            Time.timeScale = 0;
            menu.SetActive(true);
            scoreTxt.SetActive(false);
            health.SetActive(false);           
            active = true;
            
        }

        if (Input.GetKey(KeyCode.Tab) && active)
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            scoreTxt.SetActive(true);
            health.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            active = false;          
        }
    }
}

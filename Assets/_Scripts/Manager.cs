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

    public static GameObject menu1;
    public static GameObject scoreTxt1;
    public static GameObject health1;

    float maxSpawned = 3;
    public static float spawned = 0;
    bool spawnedIn = false;
    private void Start()
    {
        menu1 = menu;
        scoreTxt1 = scoreTxt;
        health1 = health;
    }

    private void Update()
    {
        if (destroyed == 5)
        {
            destroyed = 0;
            maxSpawned += 1;
            
        }

        

        if (spawned == 12)
        {
            EnemyController.shootingTime = 2.5f;
        }

        if (spawned == 18)
        {
            EnemyController.shootingTime = 2;
        }

        Debug.Log(spawned);
        Debug.Log(maxSpawned);

        SetText();
        StartCoroutine(Spawn());
        MenuActive();
    }

    void SetText()
    {
        text.SetText("score " + score);
    }

    IEnumerator Spawn()
    {
        if (!spawnedIn)
        {
            spawnedIn = true; 
            int r = Random.Range(0, spawners.Length);
            Instantiate(tank, spawners[r].transform.position, Quaternion.identity);
            spawned++;
        }
        yield return new WaitForSeconds(1);
        if (spawned != maxSpawned)
        {
            spawnedIn = false;
        }
    }

    void MenuActive()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            Time.timeScale = 0;
            menu.SetActive(true);
            scoreTxt.SetActive(false);
            health.SetActive(false);           
            
        }        
    }

    public static void MenuDeactive()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Time.timeScale = 1;
        menu1.SetActive(false);
        scoreTxt1.SetActive(true);
        health1.SetActive(true);
    }
}



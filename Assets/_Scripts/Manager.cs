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
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject scoreTxt;
    [SerializeField] GameObject health;

    public static GameObject menu1;
    public static GameObject gameOver1;
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
        gameOver1 = gameOver;
    }

    private void Update()
    {
        if (destroyed == 5)
        {
            destroyed = 0;
            maxSpawned += 1;
            
        }
        
        switch (maxSpawned)
        {
            case 10:
                EnemyController.shootingTime = 2.5f;
                break;

            case 15:
                EnemyController.shootingTime = 2;
                break;

            case 20:
                EnemyController.shootingTime = 1.5f;
                break;

            case 25:
                EnemyController.shootingTime = 1;
                break;

        }

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
            PlayerController.canRotate = false;
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
        PlayerController.canRotate = true;
        Time.timeScale = 1;
        menu1.SetActive(false);
        scoreTxt1.SetActive(true);
        health1.SetActive(true);
    }

    public static void GameOver()
    {
        if (PlayerController.health <= 0)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            PlayerController.canRotate = false;
            gameOver1.SetActive(true);
            scoreTxt1.SetActive(false);
            health1.SetActive(false);
        }
    }
}



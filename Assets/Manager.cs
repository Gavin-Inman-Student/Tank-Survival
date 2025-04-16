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
    [SerializeField] GameObject Tank;
    float spawned = 3;
    private void Start()
    {
    }

    private void Update()
    {
        if (destroyed <= spawned)
        {
            destroyed = 0;
            spawned += 1;
        }

        SetText();
        Spawn();
    }

    void SetText()
    {
        text.SetText("score" + score);
    }

    void Spawn()
    {
        for (int i = 0; i < spawned; i++)
        {
            int r = Random.Range(0, spawners.Length);
            Instantiate(Tank, spawners[r].transform.position, Quaternion.identity);
        }
    }
}

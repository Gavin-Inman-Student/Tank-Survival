using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f;

    }

    public void Resume()
    {
        Manager.MenuDeactive();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}

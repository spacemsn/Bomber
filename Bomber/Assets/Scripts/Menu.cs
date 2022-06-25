using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Меню паузы в игре")]
    private bool isPaused = false;
    public GameObject pauseMenyUI;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Нажал");
        SceneManager.LoadScene(0);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Resume()
    {
        pauseMenyUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void Paused()
    {
        pauseMenyUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}

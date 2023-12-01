using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus: MonoBehaviour
{
    [Header("All Menu's")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenuUI;
    public GameObject ObjectiveMenuUI;

    [Header("Shop")]
    public GameObject shopUI;
    public GameObject alertUseText;
    public GameObject alertBuyText;

    public static bool GameIsStopped = false;
    public static bool openShop = false;

    private void Start()
    {
        showShop();
        removeShop();

        showObjectives();
        StartCoroutine(hiddenObjectives());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsStopped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if(Input.GetKeyDown("m")) 
        {
            if(GameIsStopped)
            {
                removeObjectives();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showObjectives();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if (Input.GetKeyDown("p"))
        {
            if (openShop)
            {
                removeShop();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                showShop();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    IEnumerator hiddenObjectives()
    {
        yield return new WaitForSeconds(2);

        removeObjectives();
    }

    public void showObjectives()
    {
        ObjectiveMenuUI.SetActive(true);
        /*        Time.timeScale = 0f;*/
        GameIsStopped = true;
    }

    public void showShop()
    {

        shopUI.SetActive(true);
        openShop = true;
    }

    public void removeShop()
    {
        alertUseText.SetActive(false);
        alertBuyText.SetActive(false);
        shopUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        openShop = false;
    }

    public void removeObjectives()
    {
        ObjectiveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsStopped = false;
    }

    public void Restart()
    {
        // SceneManager.LoadScene("MainMenu");
        Application.LoadLevel(1);
    }

    public void LoadMenu()
    {
        // SceneManager.LoadScene("MainMenu");
        Application.LoadLevel(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        /*        Time.timeScale = 0f;*/
        GameIsStopped = true;
    }
}

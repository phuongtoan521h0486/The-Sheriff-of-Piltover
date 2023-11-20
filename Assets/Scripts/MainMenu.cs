using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public GameObject loadingImage;

    public Slider progressBar;

    private void Start()
    {
        loadingImage.SetActive(false);
    }

    public void OnPlayButton()
    {
        StartCoroutine(LoadSceneAsync());
        StartCoroutine(ChangeLevel());
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Menus.GameIsStopped = false;
    }

    IEnumerator LoadSceneAsync()
    {
        loadingImage.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SheriffOfPiltover");

        StartCoroutine(loadingTextDisplay());

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator loadingTextDisplay()
    {
        for(int i=0; i < 100; i=i+5)
        {
            loadingText.text = "loading... " + i + "%";
            progressBar.value = (float)i;
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void OnQuitButton()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator ChangeLevel()
    {
        GameObject fader = GameObject.Find("FaderScripts");
        FaderScript faderScript = fader.GetComponent<FaderScript>();

        float fadeTime = faderScript.BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
    }
}

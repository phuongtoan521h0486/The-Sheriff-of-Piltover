using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI loadingText;
    public GameObject loadingImage;
    public Slider progressBar;
    public GameObject optionImage;
    public GameObject shortkeysImage;
    public GameObject plotgameImage;
    public Slider sliderVolume;

    private void Start()
    {
        loadingImage.SetActive(true);
        loadingImage.SetActive(false);

        GetComponent<AudioSource>().volume = DataManager.Instance.volumeCurrent;
        sliderVolume.value = DataManager.Instance.volumeCurrent;

        if (sliderVolume == null)
        {
            sliderVolume = GetComponent<Slider>();
        }

        sliderVolume.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        GetComponent<AudioSource>().volume = volume;
        DataManager.Instance.updateVolumeCurrent(volume);
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

    public void OnOptionButton()
    {
        optionImage.SetActive(true);
    }

    public void onBackToMainButton()
    {
        optionImage.SetActive(false);
    }

    public void OnShortKeysButton()
    {
        shortkeysImage.SetActive(true);
    }

    public void onBackToOptionButton1()
    {
        shortkeysImage.SetActive(false);
    }

    public void OnPlotGameButton()
    {
        plotgameImage.SetActive(true);
    }

    public void onBackToOptionButton2()
    {
        plotgameImage.SetActive(false);
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

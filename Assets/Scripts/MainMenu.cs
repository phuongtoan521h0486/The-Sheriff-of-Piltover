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

    private void Start()
    {
        loadingImage.SetActive(false);
        loadingText.enabled = false;
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
        loadingText.enabled = true;
        loadingText.text = "loading... 0%";

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SheriffOfPiltover");

        int count = 0;
        while (!asyncLoad.isDone)
        {
            count = count + 2;
            if (count >= 100)
            {
                loadingText.text = "loading... 100%";
                count = 0;
            }

            loadingText.text = "loading... " + count + "%";

            yield return null;
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

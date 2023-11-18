using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        StartCoroutine(ChangeLevel());
        Application.LoadLevel(1);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Menus.GameIsStopped = false;
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

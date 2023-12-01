using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public float volumeCurrent = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void updateVolumeCurrent(float volume)
    {
        volumeCurrent = volume;
        if(GameController.occurrence != null)
        {
            GameController.occurrence.updateVolumeCurrent(volume);
        }
    }
}

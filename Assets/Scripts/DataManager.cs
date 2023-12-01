using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public float volumeCurrent = 1.0f;

    public static DataManager occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public float getVolumeCurrent()
    {
        return occurrence.volumeCurrent;
    }
}

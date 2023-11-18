using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesComplete : MonoBehaviour
{
    [Header("Objectives to Complete")]
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    public void GetObjectivesDone(string obj)
    {
        if(obj.Equals("task1"))
        {
            objective1.text = "1. Completed";
            objective1.color = Color.green;
        }

        //add tasks in here
    }
}

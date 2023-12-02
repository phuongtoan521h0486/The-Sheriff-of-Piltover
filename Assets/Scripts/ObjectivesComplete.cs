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
    public TextMeshProUGUI objective3;

    private float amountDefeatedZombies = 0;
    private float amountZombies;

    private bool completed1 = false;
    private bool completed2 = false;

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    private void Start()
    {
        amountZombies = ZombieSpawn.amountZombies;
    }

    public void GetObjectivesDone(string obj)
    {
        if (obj.Equals("defeated boss"))
        {
            objective3.text = "3. Completed";
            objective3.color = Color.green;
            return;
        }

        if (obj.Equals("task1"))
        {
            objective1.text = "1. Completed";
            objective1.color = Color.green;
            completed1 = true;
        }

        if (obj.Equals("task2"))
        {
            amountDefeatedZombies++;

            if(amountDefeatedZombies >= amountZombies) {
                objective2.text = "2. Completed";
                objective2.color = Color.green;
                completed2 = true;
                GameController.occurrence.completedTask2();
            }
            else
            {
                objective2.text = "2. Defeated zombies: " + amountDefeatedZombies + "/" + amountZombies;
                objective2.color = Color.red;
            }
        }

        if(completed1 && completed2)
        {
            GameController.occurrence.completedAllTasks();
        }
    }
}

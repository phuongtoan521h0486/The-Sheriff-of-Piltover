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
    private float amountCollectedCoins = 0;
    private float amountCoins;

    public static ObjectivesComplete occurrence;

    private void Awake()
    {
        occurrence = this;
    }

    private void Start()
    {
        amountZombies = ZombieSpawn.amountZombies;
        amountCoins = ZombieSpawn.amountZombies;
    }

    public void GetObjectivesDone(string obj)
    {
        if(obj.Equals("task1"))
        {
            objective1.text = "1. Completed";
            objective1.color = Color.green;
        }

        if (obj.Equals("task2"))
        {
            amountDefeatedZombies++;

            if(amountDefeatedZombies >= amountZombies) {
                objective2.text = "2. Completed";
                objective2.color = Color.green;
                GameController.occurrence.setClear(true);
            }
            else
            {
                objective2.text = "2. Defeated zombies: " + amountDefeatedZombies + "/" + amountZombies;
                objective2.color = Color.red;
            }
        }

        if (obj.Equals("task3"))
        {
            amountCollectedCoins++;

            if (amountCollectedCoins >= amountCoins)
            {
                objective3.text = "3. Completed";
                objective3.color = Color.green;
                GameController.occurrence.winGame();
            }
            else
            {
                objective3.text = "3. Collected coins: " + amountCollectedCoins + "/" + amountCoins;
                objective3.color = Color.red;
            }

            GameController.occurrence.updateAmountCoins();
        }

        //add tasks in here
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float amountZombies;
    private float count;

    private bool won = false;

    public static GameController occurrence;

    public GameObject WinGameMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        amountZombies = ZombieSpawn.amountZombies;
        Debug.Log("Amount zombies: " +  amountZombies);
        count = 0;
    }

    private void Awake()
    {
        occurrence = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= amountZombies)
        {
            if(won == false) {
                won = true;
                winGame();
            }
        }
    }

    public void winGame()
    {
        WinGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);

        Debug.Log("win game");
    }

    public void updateCount()
    {
        count++;
        Debug.Log("defeated zombies: " + count);
    }
}

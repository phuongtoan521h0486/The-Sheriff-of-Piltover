using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private float amountCoins;
    private float amountCurrent;

    private bool won = false;

    public static GameController occurrence;

    [Header("UI")]
    public GameObject WinGameMenuUI;
    public GameObject LoadingBullets;
    public Text AmmoText;

    // Start is called before the first frame update
    void Start()
    {
        AmmoText.text = "Ammo: " + Rifle.occurrence.maximumAmmunition;
        amountCoins = ZombieSpawn.amountZombies;
        Debug.Log("Amount coins need collect: " + amountCoins);
        amountCurrent = 0;
    }

    private void Awake()
    {
        occurrence = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountCurrent >= amountCoins)
        {
            if(won == false) {
                won = true;
                StartCoroutine(winGame());
            }
        }
    }

    IEnumerator winGame()
    {
        yield return new WaitForSeconds(1);

        WinGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);

        Debug.Log("win game");
    }

    public void updateAmountCoins()
    {
        amountCurrent++;
        Debug.Log("selected coins: " + amountCurrent);
    }

    public void LoadingBullets_()
    {
        AmmoText.text = "loading...";
        LoadingBullets.SetActive(true);
    }

    public void LoadedBullets_()
    {
        AmmoText.text = "Ammo: " + Rifle.occurrence.maximumAmmunition;
        LoadingBullets.SetActive(false);
    }
}
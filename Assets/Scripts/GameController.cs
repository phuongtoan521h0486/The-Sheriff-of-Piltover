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

    private bool zombiesSpawn = false;
    private GameObject[] listZombies;
    private LineRenderer lineRenderer;
    public Transform player;
    public Transform dangerPoint;

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

        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    private void Awake()
    {
        occurrence = this;
    }

    // Update is called once per frame
    void Update()
    {
        closestZombie();
        if (amountCurrent >= amountCoins)
        {
            if (won == false)
            {
                won = true;
                winGame();
            }
        }
    }

    public void winGame()
    {
        StartCoroutine(displayWinGame());
    }

    IEnumerator displayWinGame()
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

    public void setZombiesSpawn(bool status)
    {
        zombiesSpawn = status;
    }

    private void closestZombie()
    {
        if(zombiesSpawn == false)
        {
            lineRenderer.SetPosition(0, new Vector3(player.position.x, player.position.y + 1.5f, player.position.z));
            lineRenderer.SetPosition(1, dangerPoint.position);
            lineRenderer.SetWidth(0.05f, 0.05f);
            return;
        }

        listZombies = GameObject.FindGameObjectsWithTag("zombie");

        if (listZombies == null)
        {
            Debug.Log("not found");
            return;
        }

        GameObject closest = listZombies[0];

        float distance = Mathf.Infinity;

        Vector3 different;

        foreach (GameObject zombie in listZombies)
        {
            if (zombie.activeSelf)
            {
                different = zombie.transform.position - player.transform.position;

                float currentDistance = different.sqrMagnitude;

                if (currentDistance < distance)
                {
                    closest = zombie;

                    distance = currentDistance;
                }
            }
        }

        Vector3 position = new Vector3(player.position.x, player.position.y + 1.5f, player.position.z);
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, closest.transform.position);
        lineRenderer.SetWidth(0.05f, 0.05f);
    }
}
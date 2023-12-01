using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private float amountCoins;
    private float amountCurrent;

    public static bool won = false;
    private bool clear = false;

    public static GameController occurrence;

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
        lineRenderer.SetPosition(0, new Vector3(player.position.x, player.position.y + 1.5f, player.position.z));
        lineRenderer.SetPosition(1, dangerPoint.position);
        lineRenderer.SetWidth(0.05f, 0.05f);

        GetComponent<AudioSource>().volume = DataManager.Instance.volumeCurrent;
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
                GetComponent<AudioSource>().Stop();
                AudioController.occurrence.playWinGame();

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
/*        Object.Destroy(gameObject, 1.0f);*/

        Debug.Log("win game");
    }

    public void setClear(bool status)
    {
        clear = status;
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

    public void updateVolumeCurrent(float volume)
    {
        GetComponent<AudioSource>().volume = volume;
    }

    private void closestZombie()
    {
        if(clear)
        {
            Destroy(lineRenderer);
            return;
        }

        listZombies = GameObject.FindGameObjectsWithTag("zombie");

        if (listZombies == null || listZombies.Length <= 0)
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
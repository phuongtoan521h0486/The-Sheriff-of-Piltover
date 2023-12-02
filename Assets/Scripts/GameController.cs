using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool won = false;
    public static bool lose = false;
    private bool task2 = false;
    private bool bossAppear = false;
    private float radius = 20f;

    public static GameController occurrence;

    private GameObject[] listZombies;
    private LineRenderer lineRenderer;
    public Transform player;
    public Transform dangerPoint;
    

    [Header("UI")]
    public GameObject WinGameMenuUI;
    public GameObject LoseGameMenuUI;
    public GameObject MessageAlert;
    public TextMeshProUGUI MessageText;
    public Text AmmoText;

    [Header("Boss")]
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        won = false;
        lose = false;
        task2 = false;
        bossAppear = false;

        AmmoText.text = "Ammo: " + Rifle.occurrence.maximumAmmunition;

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

    void Update()
    {
        closestZombie();

        if(won == true || lose == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void winGame()
    {
        won = true;
        GetComponent<AudioSource>().Stop();
        AudioController.occurrence.playWinGame();
        StartCoroutine(displayWinGame());
    }

    IEnumerator displayWinGame()
    {
        yield return new WaitForSeconds(1);

        WinGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("win game");
    }

    public void completedTask2()
    {
        task2 = true;
        Destroy(lineRenderer);
    }

    public void completedAllTasks()
    {
        spawnBoss();
    }

    private void spawnBoss()
    {
        bossAppear = true;
        StartCoroutine(displayAlert("boss has appeared"));
        AudioController.occurrence.playSpawnBoss();

        float randomAngle = Random.Range(0f, 360f);
        float angleInRadians = Mathf.Deg2Rad * randomAngle;
        float x = player.transform.position.x + radius * Mathf.Cos(angleInRadians);
        float z = player.transform.position.z + radius * Mathf.Sin(angleInRadians);

        boss.transform.position = new Vector3(x, player.transform.position.y, z);
        boss.SetActive(true);
    }

    public void spawnZombies()
    {
        StartCoroutine(displayAlert("zombies has appeared"));
        AudioController.occurrence.playSpawnZombie();
    }

    public void defeatedBoss()
    {
        ObjectivesComplete.occurrence.GetObjectivesDone("defeated boss");
        winGame();
    }

    public void loseGame()
    {
        lose = true;
        GetComponent<AudioSource>().Stop();
        AudioController.occurrence.playLoseGame();
        LoseGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Destroy(lineRenderer);
    }

    public void LoadingBullets_()
    {
        StartCoroutine(displayLoadingBulletsAlert());
    }

    IEnumerator displayLoadingBulletsAlert()
    {
        AmmoText.text = "loading...";
        MessageAlert.SetActive(true);
        MessageText.color = Color.yellow;
        MessageText.text = "loading bullets...";
        yield return new WaitForSeconds(3.633f);
        AmmoText.text = "Ammo: " + Rifle.occurrence.maximumAmmunition;
        MessageAlert.SetActive(false);
    }

    IEnumerator displayAlert(string message)
    {
        MessageAlert.SetActive(true);
        MessageText.color = Color.red;
        MessageText.text = message;
        yield return new WaitForSeconds(2.0f);
        MessageAlert.SetActive(false);
    }

    public void updateVolumeCurrent(float volume)
    {
        GetComponent<AudioSource>().volume = volume;
    }

    private void closestZombie()
    {
        if(task2 == true || bossAppear == true || lose == true) { return; }

        listZombies = GameObject.FindGameObjectsWithTag("zombie");

        if (listZombies == null || listZombies.Length <= 0)
        {
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
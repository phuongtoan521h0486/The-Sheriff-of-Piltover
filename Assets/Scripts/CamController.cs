using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject fixedCamera;
    public GameObject spawnZombiesCamera;


    private int state;

    private void Start()
    {
        // Initially enable only one of the cameras
        mainCamera.SetActive(true);
        fixedCamera.SetActive(false);
        spawnZombiesCamera.SetActive(false);

        state = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            state = (state + 1);
            if (state > 3)
            {
                state = 1;
            }

            // Switch between cameras when the "C" key is pressed
            mainCamera.SetActive(state == 1);
            fixedCamera.SetActive(state == 2);
            spawnZombiesCamera.SetActive(state == 3);
        }
    }

}

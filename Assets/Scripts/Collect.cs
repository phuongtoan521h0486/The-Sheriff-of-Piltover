using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            AudioController.occurrence.playCollectCoin();
            ShopController.occurrence.collectCoin();
            ObjectivesComplete.occurrence.GetObjectivesDone("task3");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collect success");
            Destroy(gameObject);
            ObjectivesComplete.occurrence.GetObjectivesDone("task3");
        }
    }
}

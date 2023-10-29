using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public float speedH = 2.0f;
    public float speedV = 2.0f;


    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = player.transform.rotation;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z + -1);

        float changeX = -(player.transform.rotation.y);
        float changeZ = Mathf.Abs(player.transform.rotation.y);

        transform.position = new Vector3(transform.position.x + changeX*2, transform.position.y, transform.position.z + changeZ*2);
    }
}

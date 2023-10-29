using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    public GameObject bullet;
    public Transform bulletSp;

    public float fireRate;
    private float timeRate;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 2, 0), ForceMode.Impulse);
        }

        if (Input.GetButton("Fire1") && Time.time > timeRate)
        {
            timeRate = Time.time + fireRate;
            Instantiate(bullet, bulletSp.position, bulletSp.rotation);
            GetComponent<AudioSource>().Play();
        }

    }

    void FixedUpdate()
    {
        var hoz = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        /*GetComponent<Rigidbody>().velocity =  new Vector3(hoz, 0, ver) * speed;*/
        transform.Translate(Vector3.forward * ver * Time.deltaTime * speed);
        transform.Translate(Vector3.right * hoz * Time.deltaTime * speed);

    }
}

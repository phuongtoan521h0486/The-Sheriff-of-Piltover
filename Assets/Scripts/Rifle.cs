using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public static Rifle occurrence;

    [Header("Rifle Things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0f;
    public Animator animator;
    public PlayerScript player;
    public Transform hand;

    [Header("Rifle Ammunition and shooting")]
    public int maximumAmmunition = 20;
    public int mag = 2;
    private int presentAmmunition;
    public float reloadingTime = 3.633f;
    private bool setReloading = false;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject WoodedEffect;
    public GameObject goreEffect;

    [Header("Rifle time")]
    public float fireRate = 0.4f;
    private float timeRate;

    [Header("Cameras")]
    public Transform playerCamera;

    private void Awake()
    {
        occurrence = this;
        transform.SetParent(hand);
        presentAmmunition = maximumAmmunition;
    }
    private void Update()
    {
        if (setReloading)
        {
            return;
        }
        if (presentAmmunition <= 0 && mag > 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            if (Menus.openShop == true || Menus.GameIsStopped == true || CamController.isMainCamera == false)
            {
                return;
            }

            if (presentAmmunition <= 0) { return; }

            if (!(Input.GetButton("Fire1") && Time.time > timeRate)) { return; }

            timeRate = Time.time + fireRate;

            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = 1;
            //nextTimeToShoot = Time.time + 1f/fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", true);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
        }
    }
    private void Shoot()
    {
        if(mag == 0)
        {

            return;
        }

        Quaternion cameraRotation = playerCamera.transform.rotation;
        PlayerScript.occurrence.transform.rotation = cameraRotation; //set rotation of player

        presentAmmunition--;
        AudioController.occurrence.playFire();

        if (presentAmmunition == 0)
        {
            mag--;
        }

        AmmoCount.occurrence.UpdateAmmoText(presentAmmunition);
        AmmoCount.occurrence.UpdateMagText(mag);

        muzzleSpark.Play();
        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();
            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
            else if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGO = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGO, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGO = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGO, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading...");
        GameController.occurrence.LoadingBullets_();
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmunition = maximumAmmunition;
        player.playerSpeed = 3f;
        player.playerSprint = 5f;
        setReloading = false;
        Debug.Log("Reloaded");
        GameController.occurrence.LoadedBullets_();
    }

    public void addMag()
    {
        mag++;
        AmmoCount.occurrence.UpdateMagText(mag);
    }
}
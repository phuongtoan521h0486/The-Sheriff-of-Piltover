using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioController : MonoBehaviour
{
    public static AudioController occurrence;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip collectCoin;
    public AudioClip fire;
    public AudioClip spawnZombie;
    public AudioClip buyItem;
    public AudioClip useItem;
    public AudioClip winGame;

    private void Awake()
    {
        occurrence = this;
        audioSource = GetComponent<AudioSource>();
    }


    public void playCollectCoin()
    {
        audioSource.clip = collectCoin;
        audioSource.Play();
    }

    public void playFire()
    {
        audioSource.clip = fire;
        audioSource.Play();

    }

    public void playSpawnZombie()
    {
        audioSource.clip = spawnZombie;
        audioSource.Play();
    }

    public void playBuyItem()
    {
        audioSource.clip = buyItem;
        audioSource.Play();
    }

    public void playUseItem()
    {
        audioSource.clip = useItem;
        audioSource.Play();
    }

    public void playWinGame()
    {
        audioSource.clip = winGame;
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController occurrence;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip collectCoin;
    public AudioClip fire;
    public AudioClip zombieAttack;
    public AudioClip spawnZombie;
    public AudioClip buyItem;
    public AudioClip useItem;
    public AudioClip winGame;
    public AudioClip loseGame;

    [Header("Slider")]
    public Slider sliderVolume;

    private void Awake()
    {
        occurrence = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = DataManager.Instance.volumeCurrent;
        sliderVolume.value = DataManager.Instance.volumeCurrent;

        if (sliderVolume == null)
        {
            sliderVolume = GetComponent<Slider>();
        }

        sliderVolume.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
        DataManager.Instance.updateVolumeCurrent(volume);
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

    public void playZombieAttack()
    {
        audioSource.clip = zombieAttack;
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

    public void playLoseGame()
    {
        audioSource.clip = loseGame;
        audioSource.Play();
    }
}

using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip jumpSound;

    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void SetSFXVolume(float volume)
    {
        audioSource.volume = volume;
    }
}

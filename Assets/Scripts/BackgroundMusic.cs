using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0f, 1f);
        audioSource.volume = volume;
    }
}
